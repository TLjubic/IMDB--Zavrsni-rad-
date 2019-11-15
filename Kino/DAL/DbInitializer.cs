using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DbInitializer
    {
        public static void Initialize(KinoDb context)
        {

            if (context.Directors.Any())
            {
                return;   // DB has been seeded
            }

            var directors = new Director[]
            {
                new Director
                {
                    FirstName = "Steven", LastName = "Spielberg", Birth = DateTime.Parse("1946-12-18"),
                    Description = "Producer, director and writer"
                },
                new Director
                {
                    FirstName = "Frank", LastName = "Darabont", Birth = DateTime.Parse("1951-01-28"),
                    Description = "Producer, director and writer"
                },
                new Director
                {
                    FirstName = "Ridley", LastName = "Scott", Birth = DateTime.Parse("1937-11-30"),
                    Description = "Producer and director"
                },
                new Director
                {
                    FirstName = "Mel", LastName = "Gibson", Birth = DateTime.Parse("1956-01-07"),
                    Description = "Actor, producer and director"
                },
                new Director
                {
                    FirstName = "Todd", LastName = "Phillips", Birth = DateTime.Parse("1970-12-20"),
                    Description = "Actor and producer"
                },
                new Director
                {
                    FirstName = "James", LastName = "Cameron", Birth = DateTime.Parse("1954-08-07"),
                    Description = "Producer, writer and director"
                }

            };

            foreach (Director director in directors)
            {
                context.Directors.Add(director);
            }
            context.SaveChanges();


            var movies = new Movie[]
            {
                new Movie
                {
                    Title = "Schindler's List", ReleaseDate = DateTime.Parse("1993-09-01"),
                    Runtime = 195,
                    Description = "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually " +
                    "becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.",
                    DirectorId = 1
                }, 
                new Movie
                {
                    Title = "The Shawshank Redemption", ReleaseDate = DateTime.Parse("1994-01-01"),
                    Runtime = 142,
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption " +
                    "through acts of common decency.",
                    DirectorId = 2
                },
                new Movie
                {
                    Title = "Gladiator", ReleaseDate = DateTime.Parse("2000-10-15"),
                    Runtime = 170,
                    Description = "A former Roman General sets out to exact vengeance against the corrupt emperor who " +
                    "murdered his family and sent him into slavery.",
                    DirectorId = 3
                },
                new Movie
                {
                    Title = "Braveheart", ReleaseDate = DateTime.Parse("1995-12-11"),
                    Runtime = 182,
                    Description = "When his secret bride is executed for assaulting an English soldier who tried to rape her, " +
                    "William Wallace begins a revolt against King Edward I of England.",
                    DirectorId = 4
                },
                new Movie
                {
                    Title = "Avatar", ReleaseDate = DateTime.Parse("2009-03-22"),
                    Runtime = 165,
                    Description = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following " +
                    "his orders and protecting the world he feels is his home.",
                    DirectorId = 5
                },
                new Movie
                {
                    Title = "Joker", ReleaseDate = DateTime.Parse("2019-10-05"),
                    Runtime = 162,
                    Description = "In Gotham City, mentally-troubled comedian Arthur Fleck is disregarded and mistreated by society. " +
                    "He then embarks on a downward spiral of revolution and bloody crime. This path brings him face-to-face with his " +
                    "alter-ego: The Joker.", 
                    DirectorId = 6
                }
            };

            foreach (Movie movie in movies)
            {
                context.Movies.Add(movie);
            }
            context.SaveChanges();


            var tvShows = new TVShow[]
            {
                new TVShow
                {
                    Title = "The Walking Dead", ReleaseDate = DateTime.Parse("2010-09-01"),
                    NumberOfEpisode = 148,
                    Description = "Sheriff Deputy Rick Grimes wakes up from a coma to " +
                    "learn the world is in ruins, and must lead a group of survivors to stay alive."
                },
                new TVShow
                {
                    Title = "Breaking Bad", ReleaseDate = DateTime.Parse("2008-01-01"),
                    NumberOfEpisode = 62,
                    Description = "A high school chemistry teacher diagnosed with inoperable lung cancer " +
                    "turns to manufacturing and selling methamphetamine in order to secure his family's future."
                },
                new TVShow
                {
                    Title = "Game of Thrones", ReleaseDate = DateTime.Parse("2011-10-15"),
                    NumberOfEpisode = 73,
                    Description = "Nine noble families fight for control over the mythical lands of Westeros, " +
                    "while an ancient enemy returns after being dormant for thousands of years."
                },
                new TVShow
                {
                    Title = "La Casa de Papel", ReleaseDate = DateTime.Parse("2017-12-11"),
                    NumberOfEpisode = 26,
                    Description = "A group of unique robbers assault the Factory of Moneda and Timbre to carry out " +
                    "the most perfect robbery in the history of Spain and take home 2.4 billion euros."
                },
                new TVShow
                {
                    Title = "Peaky Blinders", ReleaseDate = DateTime.Parse("2013-03-22"),
                    NumberOfEpisode = 37,
                    Description = "A gangster family epic set in 1919 Birmingham, England; centered on a gang who sew razor " +
                    "blades in the peaks of their caps, and their fierce boss Tommy Shelby."
                },
                new TVShow
                {
                    Title = "The Haunting of Hill House", ReleaseDate = DateTime.Parse("2018-10-05"),
                    NumberOfEpisode = 10,
                    Description = "Flashing between past and present, a fractured family confronts haunting memories of their " +
                    "old home and the terrifying events that drove them from it."
                },
                new TVShow
                {
                    Title = "Vikings", ReleaseDate = DateTime.Parse("2013-10-05"),
                    NumberOfEpisode = 89,
                    Description = "Vikings transports us to the brutal and mysterious world of Ragnar Lothbrok, a Viking warrior and " +
                    "farmer who yearns to explore - and raid - the distant shores across the ocean."
                },
                new TVShow
                {
                    Title = "The Wire", ReleaseDate = DateTime.Parse("2002-10-05"),
                    NumberOfEpisode = 60,
                    Description = "The Baltimore drug scene, as seen through the eyes of drug dealers and law enforcement."
                }
            };

            foreach (TVShow tvShow in tvShows)
            {
                context.TVShows.Add(tvShow);
            }
            context.SaveChanges();



            var stars = new Star[]
            {
                new Star
                {
                    FirstName = "Liam", LastName = "Neeson", Birth = DateTime.Parse("1952-07-22"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Tim", LastName = "Robbins", Birth = DateTime.Parse("1958-10-25"),
                    Description = "Actor and director"
                },
                new Star
                {
                    FirstName = "Morgan", LastName = "Freeman", Birth = DateTime.Parse("1937-07-01"),
                    Description = "Actor, producer and director"
                },
                new Star
                {
                    FirstName = "Russell", LastName = "Crowe", Birth = DateTime.Parse("1964-04-12"),
                    Description = "Actor and soundtrack"
                },
                new Star
                {
                    FirstName = "Joaquin", LastName = "Phoenix", Birth = DateTime.Parse("1974-10-28"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Mel", LastName = "Gibson", Birth = DateTime.Parse("1956-01-07"),
                    Description = "Actor, producer and director"
                },
                new Star
                {
                    FirstName = "Sam", LastName = "Worthington", Birth = DateTime.Parse("1976-08-19"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Andrew", LastName = "Lincoln", Birth = DateTime.Parse("1973-02-10"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Norman", LastName = "Reedus", Birth = DateTime.Parse("1969-01-09"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Bryan", LastName = "Cranston", Birth = DateTime.Parse("1956-02-10"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Aaron", LastName = "Paul", Birth = DateTime.Parse("1979-10-22"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Emilia", LastName = "Clarke", Birth = DateTime.Parse("1986-10-11"),
                    Description = "Actress and producer"
                },
                new Star
                {
                    FirstName = "Peter", LastName = "Dinklage", Birth = DateTime.Parse("1969-10-23"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Kit", LastName = "Harington", Birth = DateTime.Parse("1986-12-08"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Úrsula", LastName = "Corberó", Birth = DateTime.Parse("1989-11-08"),
                    Description = "Actress and producer"
                },
                new Star
                {
                    FirstName = "Cillian", LastName = "Murphy", Birth = DateTime.Parse("1976-10-23"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Henry", LastName = "Thomas", Birth = DateTime.Parse("1971-07-09"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Gustaf", LastName = "Skarsgård", Birth = DateTime.Parse("1980-11-12"),
                    Description = "Actor, producer and writer"
                },
                new Star
                {
                    FirstName = "Katheryn", LastName = "Winnick", Birth = DateTime.Parse("1977-12-17"),
                    Description = "Actress, producer and writer"
                },
                new Star
                {
                    FirstName = "Alexander", LastName = "Ludwig", Birth = DateTime.Parse("1992-07-07"),
                    Description = "Actor and producer"
                },
                new Star
                {
                    FirstName = "Dominic", LastName = "West", Birth = DateTime.Parse("1969-10-15"),
                    Description = "Actor and producer"
                }

            };

            foreach (Star star in stars)
            {
                context.Stars.Add(star);
            }
            context.SaveChanges();




            var movieGenres = new MovieGenre[]
            {
                new MovieGenre
                {
                    MovieId = movies.Single(m => m.Title == "Schindler's List").MovieId,
                    GenreId = 2
                },
                new MovieGenre
                {
                    MovieId = movies.Single(m => m.Title == "Schindler's List").MovieId,
                    GenreId = 6
                },
                new MovieGenre
                {
                    MovieId = movies.Single(m => m.Title == "The Shawshank Redemption").MovieId,
                    GenreId = 6
                },
                new MovieGenre
                {
                    MovieId = movies.Single(m => m.Title == "Gladiator").MovieId,
                    GenreId = 6
                },
                new MovieGenre
                {
                    MovieId = movies.Single(m => m.Title == "Braveheart").MovieId,
                    GenreId = 2
                },
                new MovieGenre
                {
                    MovieId = movies.Single(m => m.Title == "Braveheart").MovieId,
                    GenreId = 6
                },
                new MovieGenre
                {
                    MovieId = movies.Single(m => m.Title == "Avatar").MovieId,
                    GenreId = 4
                },
                new MovieGenre
                {
                    MovieId = movies.Single(m => m.Title == "Joker").MovieId,
                    GenreId = 6
                },
                new MovieGenre
                {
                    MovieId = movies.Single(m => m.Title == "Joker").MovieId,
                    GenreId = 1
                },
            };

            foreach (MovieGenre movieGenre in movieGenres)
            {
                context.MovieGenres.Add(movieGenre);
            }
            context.SaveChanges();



            var movieStars = new MovieStar[]
            {
                new MovieStar
                {
                    MovieId = movies.Single(m => m.Title == "Schindler's List").MovieId,
                    StarId = stars.Single(s => s.FirstName == "Liam").StarId
                },
                new MovieStar
                {
                    MovieId = movies.Single(m => m.Title == "The Shawshank Redemption").MovieId,
                    StarId = stars.Single(s => s.FirstName == "Morgan").StarId
                },
                new MovieStar
                {
                    MovieId = movies.Single(m => m.Title == "The Shawshank Redemption").MovieId,
                    StarId = stars.Single(s => s.FirstName == "Tim").StarId
                },
                new MovieStar
                {
                    MovieId = movies.Single(m => m.Title == "Gladiator").MovieId,
                    StarId = stars.Single(s => s.FirstName == "Russell").StarId
                },
                new MovieStar
                {
                    MovieId = movies.Single(m => m.Title == "Gladiator").MovieId,
                    StarId = stars.Single(s => s.FirstName == "Joaquin").StarId
                },
                new MovieStar
                {
                    MovieId = movies.Single(m => m.Title == "Braveheart").MovieId,
                    StarId = stars.Single(s => s.FirstName == "Mel").StarId
                },
                new MovieStar
                {
                    MovieId = movies.Single(m => m.Title == "Joker").MovieId,
                    StarId = stars.Single(s => s.FirstName == "Joaquin").StarId
                },
                new MovieStar
                {
                    MovieId = movies.Single(m => m.Title == "Avatar").MovieId,
                    StarId = stars.Single(s => s.FirstName == "Sam").StarId
                },
            };

            foreach (MovieStar movieStar in movieStars)
            {
                context.MovieStars.Add(movieStar);
            }
            context.SaveChanges();



            var tvShowGenres = new TVShowGenre[]
            {
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "The Walking Dead").TVShowId,
                    GenreId = 6
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "The Walking Dead").TVShowId,
                    GenreId = 7
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "The Walking Dead").TVShowId,
                    GenreId = 8
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "Breaking Bad").TVShowId,
                    GenreId = 6
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "Breaking Bad").TVShowId,
                    GenreId = 1
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "Breaking Bad").TVShowId,
                    GenreId = 8
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "Game of Thrones").TVShowId,
                    GenreId = 4
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "Game of Thrones").TVShowId,
                    GenreId = 6
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "La Casa de Papel").TVShowId,
                    GenreId = 1
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "Peaky Blinders").TVShowId,
                    GenreId = 1
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "Peaky Blinders").TVShowId,
                    GenreId = 6
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "The Haunting of Hill House").TVShowId,
                    GenreId = 6
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "The Haunting of Hill House").TVShowId,
                    GenreId = 7
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "Vikings").TVShowId,
                    GenreId = 4
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "Vikings").TVShowId,
                    GenreId = 6
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "The Wire").TVShowId,
                    GenreId = 1
                },
                new TVShowGenre
                {
                    TVShowId = tvShows.Single(m => m.Title == "The Wire").TVShowId,
                    GenreId = 6
                }
            };

            foreach (TVShowGenre tvShowGenre in tvShowGenres)
            {
                context.TVShowGenres.Add(tvShowGenre);
            }
            context.SaveChanges();


            var tvShowStars = new TVShowStar[]
            {
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "The Walking Dead").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Andrew").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "The Walking Dead").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Norman").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "Breaking Bad").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Bryan").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "Breaking Bad").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Aaron").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "Game of Thrones").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Emilia").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "Game of Thrones").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Peter").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "Game of Thrones").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Kit").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "La Casa de Papel").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Úrsula").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "Peaky Blinders").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Cillian").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "The Haunting of Hill House").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Henry").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "Vikings").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Gustaf").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "Vikings").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Katheryn").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "Vikings").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Alexander").StarId
                },
                new TVShowStar
                {
                    TVShowId = tvShows.Single(m => m.Title == "The Wire").TVShowId,
                    StarId = stars.Single(s => s.FirstName == "Dominic").StarId
                },
            };

            foreach (TVShowStar tvShowStar in tvShowStars)
            {
                context.TVShowStars.Add(tvShowStar);
            }
            context.SaveChanges();

        }
    }
}
