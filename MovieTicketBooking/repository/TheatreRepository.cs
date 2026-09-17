using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.models;

namespace MovieTicketBooking.repository
{
    public class TheatreRepository
    {
        public Dictionary<string, Theatre> map = new();
        public void Save(Theatre theatre)
        {
            map.Add(theatre.Id, theatre);
        }
        public Theatre? Get(string id)
        {
            return map.TryGetValue(id, out Theatre? theatre) ? theatre : null;
        }
    }
}