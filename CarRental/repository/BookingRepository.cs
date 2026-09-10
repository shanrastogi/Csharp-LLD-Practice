using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.model;

namespace CarRental.repository
{
    public class BookingRepository
    {
        private readonly Dictionary<string, Booking> _bookings = new();
        public void AddBooking(Booking booking)
        {
            _bookings[booking.BookingId] = booking;
        }

        public Booking? GetBookingById(string bookingId)
        {
            if (_bookings.TryGetValue(bookingId, out var booking))
            {
                return booking;
            }
            return null;
        }

        public void RemoveBooking(string bookingId)
        {
            if (_bookings.Remove(bookingId, out var booking))
            {
                if (booking.Vehicle != null)
                {
                    booking.Vehicle.IsBooked = false;
                }
            }
        }

        public List<Booking> GetAllBookings()
        {
            return _bookings.Values.ToList();
        }
    }
}