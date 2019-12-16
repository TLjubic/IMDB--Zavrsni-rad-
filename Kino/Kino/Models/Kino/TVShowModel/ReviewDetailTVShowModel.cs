using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino.TVShowModel
{
    public class ReviewDetailTVShowModel : ViewModel
    {
        public TVShow TVShow { get; set; }
        public TVShowReview TVShowReview { get; set; }
    }
}
