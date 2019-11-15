using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class MovieStar
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int StarId { get; set; }
        public Star Star { get; set; }

    }
}
