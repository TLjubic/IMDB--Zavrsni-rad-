using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class KinoDb : DbContext
    {

        public KinoDb(DbContextOptions options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Director> Directors{ get; set; }
        public DbSet<MovieStar> MovieStars { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<TVShowGenre> TVShowGenres { get; set; }
        public DbSet<TVShowStar> TVShowStars { get; set; }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieStar>().HasKey(ms => new { ms.MovieId, ms.StarId });
            modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<TVShowStar>().HasKey(ts => new { ts.TVShowId, ts.StarId });
            modelBuilder.Entity<TVShowGenre>().HasKey(tg => new { tg.TVShowId, tg.GenreId });

            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 1, Name = "Crime" });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 2, Name = "War" });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 3, Name = "Comedy" });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 4, Name = "Adventure" });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 5, Name = "Family" });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 6, Name = "Drama" });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 7, Name = "Horror" });
            modelBuilder.Entity<Genre>().HasData(new Genre { GenreId = 8, Name = "Thriller" });

        }

    }
}
