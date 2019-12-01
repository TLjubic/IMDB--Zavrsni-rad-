using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino
{
    public class TVShowStarModel
    {
        public TVShow TVShow { get; set; }
        public List<int> selectedStars { get; set; }
        public IFormFile Image { get; set; }
    }
}
