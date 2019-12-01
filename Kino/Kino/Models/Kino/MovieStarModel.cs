using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino
{
    public class MovieStarModel
    {
        public Movie Movie { get; set; }
        public List<int> selectedStars { get; set; }
        public IFormFile Image { get; set; }
    }
}
