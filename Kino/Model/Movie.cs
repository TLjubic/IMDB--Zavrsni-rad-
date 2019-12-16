using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Movie
    {
        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        public int Runtime { get; set; }

        public string ImageUrl { get; set; }
        //public string Video { get; set; }

        //public double Rating { get; set; }
        //public bool OscarWinner { get; set; }

        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Director))]
        public int DirectorId { get; set; }
        public Director Director { get; set; }

        public ICollection<MovieGenre> ListOfGenres { get; set; }
        public ICollection<MovieStar> ListOfStars { get; set; }
        public ICollection<MovieReview> ListOfReviews { get; set; }

    }
}
