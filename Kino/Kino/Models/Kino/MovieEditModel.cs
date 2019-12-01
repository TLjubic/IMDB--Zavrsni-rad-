using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino
{
    public class MovieEditModel
    {
        public Movie Movie { get; set; }
        public IFormFile Image { get; set; }
    }
}
