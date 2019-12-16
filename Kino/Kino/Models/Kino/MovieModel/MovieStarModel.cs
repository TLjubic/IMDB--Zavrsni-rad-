using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino.MovieModel
{
    public class MovieStarModel : ViewModel
    {
        public Movie Movie { get; set; }
        public List<int> selectedStars { get; set; }
        public AddingMultimediaModel Multimedia { get; set; }
    }
}
