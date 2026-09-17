using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.models;
using MovieTicketBooking.repository;

namespace MovieTicketBooking.service
{
    public class ShowService
    {
        public ShowRepository _showRepository;
        public MovieRepository _movieRepository;
        public TheatreRepository _theatreRepository;

        public ShowService(ShowRepository showRepository, MovieRepository movieRepository, TheatreRepository theatreRepository)
        {
            _showRepository = showRepository;
            _movieRepository = movieRepository;
            _theatreRepository = theatreRepository;

        }

        public Show CreateShow(string showId, string movieId, string theatreId, string screenId, DateTime start, DateTime end)
        {
            Movie movie = _movieRepository.Get(movieId)
                ?? throw new InvalidOperationException($"Movie with id '{movieId}' was not found.");
            Theatre theatre = _theatreRepository.Get(theatreId)
                ?? throw new InvalidOperationException($"Theatre with id '{theatreId}' was not found.");
            Screen screen = theatre.GetScreen(screenId)
                ?? throw new InvalidOperationException($"Screen with id '{screenId}' was not found in theatre '{theatreId}'.");

            Show show = new Show(showId, movie, start, end, screen, theatre);
            _showRepository.Save(show);
            return show;
        }

        public Show? GetShow(string showId)
        {
            return _showRepository.Get(showId);
        }

        public List<Show> GetShowByMovieTitle(string movieTitle)
        {
            return _showRepository.GetAll()
                .Where(show => show.Movie.Title == movieTitle)
                .ToList();
        }
    }
}