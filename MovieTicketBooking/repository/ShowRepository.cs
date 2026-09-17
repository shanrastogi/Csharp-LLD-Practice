using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.models;

namespace MovieTicketBooking.repository
{
    public class ShowRepository
    {
        public Dictionary<string, Show> map = new();
        public void Save(Show show)
        {
            map.Add(show.Id, show);
        }

        public Show? Get(string id)
        {
            return map.TryGetValue(id, out Show? show) ? show : null;
        }

        public List<Show> GetAll()
        {
            return map.Values.ToList();
        }
    }
}