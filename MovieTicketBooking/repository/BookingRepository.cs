using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.models;

namespace MovieTicketBooking.repository
{
    public class BookingRepository
    {
        public Dictionary<string, Booking> map = new();

        public void Save(Booking booking)
        {
            map.Add(booking.BookingId, booking);
        }
    }
}