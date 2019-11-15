using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TVShowStar
    {
        public int TVShowId { get; set; }
        public TVShow TVShow { get; set; }

        public int StarId { get; set; }
        public Star Star { get; set; }
    }
}
