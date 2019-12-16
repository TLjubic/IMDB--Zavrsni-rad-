using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.Models.Kino
{
    public class AddingMultimediaModel
    {
        public IFormFile Image { get; set; }

        public string AddingImageUrl(IFormFile img, IHostingEnvironment _hostingEnvironment)
        {
            string uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            var fileName = Guid.NewGuid().ToString() + "_" + img.FileName;
            string filePath = Path.Combine(uploadFile, fileName);
            img.CopyTo(new FileStream(filePath, FileMode.Create));

            return fileName;
        }
    }
}
