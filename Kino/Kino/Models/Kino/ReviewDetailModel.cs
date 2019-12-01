using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino
{
    public class ReviewDetailModel
    {
        public Movie Movie { get; set; }
        public MovieReview MovieReview { get; set; }
        public TVShow TVShow { get; set; }
        public TVShowReview TVShowReview { get; set; }
    }
}
