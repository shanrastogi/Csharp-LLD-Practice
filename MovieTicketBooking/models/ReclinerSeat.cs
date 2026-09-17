using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.enums;

namespace MovieTicketBooking.models
{
    public class ReclinerSeat : Seat
    {
        public ReclinerSeat(string _id, double _price) : base(_id, _price)
        {
        }

        public SeatType GetSeatType()
        {
            return SeatType.RECLINER;
        }
    }
}