using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.models
{
    public abstract class Seat
    {
        public string Id { get; }
        public double Price { get; }

        public Seat(string _id, double _price)
        {
            Id = _id;
            Price = _price;
        }
    }
}