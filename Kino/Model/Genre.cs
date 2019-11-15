using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        IEnumerable<MovieGenre> ListOfMovies { get; set; }
    }
}
