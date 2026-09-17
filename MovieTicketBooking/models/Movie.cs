using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.models
{
    public class Movie
    {
        public string Id { get; }
        public string Title { get; }
        public int Duration { get; }

        public Movie(string _id, string _title, int _duration)
        {
            Id = _id;
            Title = _title;
            Duration = _duration;
        }
    }
}