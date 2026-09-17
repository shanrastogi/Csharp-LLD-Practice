using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.enums;

namespace MovieTicketBooking.models
{
    public class Booking
    {
        public string BookingId { get; }
        public string UserId { get; }
        public string ShowId { get; }
        public List<string> SeatIds { get; }
        public BookingStatus Status { get; private set; }
        public PaymentType PaymentType { get; private set; }
        public double Amount { get; }


        public Booking(string bookingId, string userId, string showId, List<string> seatIds, BookingStatus status, PaymentType paymentType, double amount)
        {
            BookingId = bookingId;
            UserId = userId;
            ShowId = showId;
            SeatIds = seatIds;
            Status = status;
            PaymentType = paymentType;
            Amount = amount;
        }

        public void Confirm(PaymentType paymentType)
        {
            PaymentType = paymentType;
            Status = BookingStatus.CONFIRMED;
        }

        public override string ToString()
        {
            return $"BookingId {BookingId}, UserId {UserId}, SeatIds {SeatIds}, ShowId {ShowId}, Status {Status}, PaymentType {PaymentType}, Amount {Amount}";
        }
    }
}