using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino.MovieModel
{
    public class ReviewDetailMovieModel : ViewModel
    {
        public Movie Movie { get; set; }

        [BindProperty]
        public MovieReview MovieReview { get; set; }

    }
}
