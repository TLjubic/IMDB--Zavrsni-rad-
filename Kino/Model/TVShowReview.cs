using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class TVShowReview
    {
        public int TVShowReviewId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public string Body { get; set; }

        [ForeignKey(nameof(TVShow))]
        public int TVShowId { get; set; }
        public TVShow TVShow { get; set; }
    }
}
