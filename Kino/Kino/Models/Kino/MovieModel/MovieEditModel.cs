using DAL;
using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino.MovieModel
{
    public class MovieEditModel : ViewModel
    {
        public Movie Movie { get; set; }
        public AddingMultimediaModel Multimedia { get; set; }
    }
}
