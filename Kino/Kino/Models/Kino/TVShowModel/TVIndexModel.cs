using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino.TVShowModel
{
    public class TVIndexModel : ViewModel
    {
        public IEnumerable<TVShow> TVShows { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Star> Stars { get; set; }
    }
}
