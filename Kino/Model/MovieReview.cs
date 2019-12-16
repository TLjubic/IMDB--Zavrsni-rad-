using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class MovieReview
    {
        public int MovieReviewId { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string Title { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public string Body { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
