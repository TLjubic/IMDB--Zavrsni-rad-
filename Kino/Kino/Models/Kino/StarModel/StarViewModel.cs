using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino.StarModel
{
    public class StarViewModel : ViewModel
    {
        public Star Star { get; set; }
        public AddingMultimediaModel Multimedia { get; set; }
        public IEnumerable<Star> Stars { get; set; }
    }
}
