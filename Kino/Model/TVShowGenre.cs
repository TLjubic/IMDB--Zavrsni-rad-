using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TVShowGenre
    {
        public int TVShowId { get; set; }
        public TVShow TVShow { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
