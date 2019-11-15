using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class TVShow
    {
        public int TVShowId { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Number of episodes")]
        public int NumberOfEpisode { get; set; }

        //public string Img { get; set; }
        //public string Video { get; set; }

        //public double Rating { get; set; }
        //public Director Director { get; set; }

        public string Description { get; set; }

        public ICollection<TVShowGenre> ListOfGenres { get; set; }
        public ICollection<TVShowStar> ListOfStars { get; set; }


    }
}
