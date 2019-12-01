using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino
{
    public class StarViewModel
    {
        public Star Star { get; set; }
        public IFormFile Image { get; set; }
    }
}
