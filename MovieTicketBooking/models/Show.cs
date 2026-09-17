using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.models
{
    public class Show
    {
        public string Id { get; }
        public Movie Movie { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
        public Screen Screen { get; }
        public Theatre Theatre { get; }

        public Show(string _id, Movie _movie, DateTime _start, DateTime _end, Screen _screen, Theatre _theatre)
        {
            Id = _id;
            Movie = _movie;
            Start = _start;
            End = _end;
            Screen = _screen;
            Theatre = _theatre;
        }

        public List<Seat> GetSeats()
        {
            return Screen.GetSeats();
        }

        public override string ToString()
        {
            return $"Id {Id}, Movie {Movie.Title}, Start {Start}, End {End}, Screen {Screen.Id}, Theatre {Theatre.Id}";
        }
    }
}