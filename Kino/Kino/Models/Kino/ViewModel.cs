using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino
{
    public class ViewModel
    {
        public IEnumerable<Movie> ListOfMovies { get; set; }
        public IEnumerable<TVShow> ListOfTVShows { get; set; }
        public IEnumerable<Star> ListOfStars { get; set; }
        public IEnumerable<Director> ListOfDirectors { get; set; }

        public ViewModel fillMenuItems(KinoDb _context, ViewModel model)
        {
            model.ListOfMovies = _context.Movies
                .ToList();

            model.ListOfTVShows = _context.TVShows
                .ToList();

            model.ListOfStars = _context.Stars
                .ToList();

            model.ListOfDirectors = _context.Directors
                .ToList();

            return model;
        }
    }
}
