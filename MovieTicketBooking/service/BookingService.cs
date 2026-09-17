using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using factory;
using MovieTicketBooking.enums;
using MovieTicketBooking.models;
using MovieTicketBooking.repository;
using MovieTicketBooking.strategy.locking;
using strategy.payment;

namespace MovieTicketBooking.service
{
    public class BookingService
    {
        private const long LockTtlMs = 5000;
        private readonly BookingRepository _bookingRepository;
        private readonly LockProvider _lockProvider;

        public BookingService(BookingRepository bookingRepository, LockProvider lockProvider)
        {
            _bookingRepository = bookingRepository;
            _lockProvider = lockProvider;
        }

        public Booking CreateBooking(string userId, Show show, List<string> seatIds)
        {
            List<string> lockedKeys = new();

            try
            {
                foreach (string seatId in seatIds)
                {
                    string lockKey = show.Id + seatId;
                    if (!_lockProvider.TryLock(lockKey, userId, LockTtlMs))
                    {
                        throw new InvalidOperationException($"Seat '{seatId}' is already locked.");
                    }

                    lockedKeys.Add(lockKey);
                }

                Dictionary<string, Seat> seats = show.GetSeats().ToDictionary(seat => seat.Id);
                double amount = seatIds.Sum(seatId =>
                {
                    if (!seats.TryGetValue(seatId, out Seat? seat))
                    {
                        throw new InvalidOperationException($"Seat '{seatId}' does not exist in show '{show.Id}'.");
                    }

                    return seat.Price;
                });

                Booking booking = new Booking(
                    Guid.NewGuid().ToString(),
                    userId,
                    show.Id,
                    new List<string>(seatIds),
                    BookingStatus.CREATED,
                    PaymentType.UPI,
                    amount);

                _bookingRepository.Save(booking);
                return booking;
            }
            catch
            {
                foreach (string lockKey in lockedKeys)
                {
                    _lockProvider.Unlock(lockKey);
                }

                throw;
            }
        }

        public Booking ConfirmBooking(Booking booking, PaymentType paymentType)
        {
            foreach (string seatId in booking.SeatIds)
            {
                string lockKey = booking.ShowId + seatId;
                if (!_lockProvider.IsLockedBy(lockKey, booking.UserId))
                {
                    throw new InvalidOperationException($"Seat '{seatId}' is no longer locked by the booking user.");
                }
            }

            IPaymentStrategy paymentStrategy = PaymentStrategyFactory.Get(paymentType);
            if (!paymentStrategy.Pay(booking.Amount))
            {
                throw new InvalidOperationException("Payment failed.");
            }

            foreach (string seatId in booking.SeatIds)
            {
                _lockProvider.Unlock(booking.ShowId + seatId);
            }

            booking.Confirm(paymentType);
            return booking;
        }
    }
}