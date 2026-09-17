using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.enums;

namespace MovieTicketBooking.models
{
    public class Screen
    {
        public string Id { get; }
        public Dictionary<string, Seat> Seats = new();

        public Screen(string _id)
        {
            Id = _id;
        }

        public void AddSeat(Seat seat)
        {
            Seats.Add(seat.Id, seat);
        }

        public List<Seat> GetSeats()
        {
            return Seats.Values.ToList();
        }
    }
}