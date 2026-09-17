using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.models;

namespace MovieTicketBooking.repository
{
    public class MovieRepository
    {
        public Dictionary<string, Movie> map = new();
        public void Save(Movie movie)
        {
            map.Add(movie.Id, movie);
        }
        public Movie? Get(string id)
        {
            return map.TryGetValue(id, out Movie? movie) ? movie : null;

        }
    }
}