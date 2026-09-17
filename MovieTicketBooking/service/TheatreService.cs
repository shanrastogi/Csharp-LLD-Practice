using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.models;
using MovieTicketBooking.repository;

namespace MovieTicketBooking.service
{
    public class TheatreService
    {
        public TheatreRepository _theatreRepository;

        public TheatreService(TheatreRepository theatreRepository)
        {
            _theatreRepository = theatreRepository;
        }

        public Theatre CreateTheatre(String id, String name)
        {
            Theatre theatre = new Theatre(id, name);
            _theatreRepository.Save(theatre);
            return theatre;
        }

        public Theatre? GetTheatre(string id)
        {
            return _theatreRepository.Get(id);
        }
    }
}