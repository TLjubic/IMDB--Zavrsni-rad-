using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino.DirectorModel
{
    public class DirectorViewModel : ViewModel
    {
        public Director Director { get; set; }
        public AddingMultimediaModel Multimedia { get; set; }
        public IEnumerable<Director> Directors { get; set; }
    }
}
