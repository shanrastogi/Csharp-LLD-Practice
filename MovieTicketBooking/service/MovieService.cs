using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.models;
using MovieTicketBooking.repository;

namespace MovieTicketBooking.service
{
    public class MovieService
    {
        public MovieRepository _movieRepository;

        public MovieService(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Movie CreateMovie(String id, String title, int duration)
        {
            Movie movie = new Movie(id, title, duration);
            _movieRepository.Save(movie);
            return movie;
        }

        public Movie? GetMovie(string id)
        {
            return _movieRepository.Get(id);
        }
    }
}