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
                    Description = "One of the most influential personalities in the history of cinema, " +
                    "Steven Spielberg is Hollywood's best known director and one of the wealthiest filmmakers in the world. " +
                    "He has an extraordinary number of commercially successful and critically acclaimed credits to his name, " +
                    "either as a director, producer or writer since launching the summer blockbuster with Jaws (1975), " +
                    "and he has done more to define popular film-making since the mid-1970s than anyone else.",
                    ImageUrl = "b82a7731-7ef1-47fc-96ba-700610306e8b_StevenSpielberg.jpg"
                },
                new Director
                {
                    FirstName = "Frank", LastName = "Darabont", Birth = DateTime.Parse("1951-01-28"),
                    Description = "Three-time Oscar nominee Frank Darabont was born in a refugee camp in 1959 in Montbeliard, " +
                    "France, the son of Hungarian parents who had fled Budapest during the failed 1956 Hungarian revolution. " +
                    "Brought to America as an infant, he settled with his family in Los Angeles and attended Hollywood High School. " +
                    "His first job in movies was as a production assistant on the 1981 low-budget film, Hell Night (1981), " +
                    "starring Linda Blair. ",
                    ImageUrl = "c7eaae5b-fd57-4740-a58e-5c10d961a5d3_FrankDarabont.jpg"
                },
                new Director
                {
                    FirstName = "Ridley", LastName = "Scott", Birth = DateTime.Parse("1937-11-30"),
                    Description = "Described by film producer Michael Deeley as the very best eye in the business, " +
                    "director Ridley Scott was born on November 30, 1937 in South Shields, Tyne and Wear (then County Durham). " +
                    "His father was an officer in the Royal Engineers and the family followed him as his career posted him throughout " +
                    "the United Kingdom and Europe before they eventually returned to Teesside. Scott wanted to join the Royal Army " +
                    "(his elder brother Frank had already joined the Merchant Navy) but his father encouraged him to develop his " +
                    "artistic talents instead and so he went to West Hartlepool College of Art and then London's Royal College of " +
                    "Art where he helped found the film department.",
                    ImageUrl = "a12adc3c-57f5-4162-89da-8d5e60f7b4b7_RidleyScott.jpg"
                },
                new Director
                {
                    FirstName = "Mel", LastName = "Gibson", Birth = DateTime.Parse("1956-01-07"),
                    Description = "Mel Columcille Gerard Gibson was born January 3, 1956 in Peekskill, New York, USA, " +
                    "as the sixth of eleven children of Hutton Gibson, a railroad brakeman, and Anne Patricia (Reilly) " +
                    "Gibson (who died in December of 1990). His mother was Irish, from County Longford, while his American-born " +
                    "father is of mostly Irish descent.",
                    ImageUrl = "b9e63498-277b-4551-bc48-6327d685f69d_MelGibson.jpg"
                },
                new Director
                {
                    FirstName = "Todd", LastName = "Phillips", Birth = DateTime.Parse("1970-12-20"),
                    Description = "Todd Phillips was born on December 20, 1970 in Brooklyn, New York City, New York, USA as Todd Bunzl. " +
                    "He is a producer and director, known for Joker (2019), Old School (2003) and Due Date (2010).",
                    ImageUrl = "87597ecf-ebab-4af4-a534-eab0672e29e8_ToddPhillips.jpg"
                },
                new Director
                {
                    FirstName = "James", LastName = "Cameron", Birth = DateTime.Parse("1954-08-07"),
                    Description = "James Francis Cameron was born on August 16, 1954 in Kapuskasing, Ontario, Canada. " +
                    "He moved to the United States in 1971. The son of an engineer, he majored in physics at California State " +
                    "University before switching to English, and eventually dropping out. He then drove a truck to support his " +
                    "screenwriting ambition. ",
                    ImageUrl = "3bbc6b2c-2896-4e79-bc84-2aab94053f2d_JamesCameron.jpg"
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
                    DirectorId = 1,
                    ImageUrl = "977f30d4-1542-4472-accd-5712bf8c26ac_Schindlers_list.jpg"
                }, 
                new Movie
                {
                    Title = "The Shawshank Redemption", ReleaseDate = DateTime.Parse("1994-01-01"),
                    Runtime = 142,
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption " +
                    "through acts of common decency.",
                    DirectorId = 2,
                    ImageUrl = "7e97b547-45de-4206-a646-4cd23f8c9872_TheShawshankRedemption.jpg"
                },
                new Movie
                {
                    Title = "Gladiator", ReleaseDate = DateTime.Parse("2000-10-15"),
                    Runtime = 170,
                    Description = "A former Roman General sets out to exact vengeance against the corrupt emperor who " +
                    "murdered his family and sent him into slavery.",
                    DirectorId = 3,
                    ImageUrl = "691b3655-3daa-4a1e-910d-19077a9aca60_Gladiator.jpg"
                },
                new Movie
                {
                    Title = "Braveheart", ReleaseDate = DateTime.Parse("1995-12-11"),
                    Runtime = 182,
                    Description = "When his secret bride is executed for assaulting an English soldier who tried to rape her, " +
                    "William Wallace begins a revolt against King Edward I of England.",
                    DirectorId = 4,
                    ImageUrl = "093b27f3-6482-4628-973c-1bd4cd007360_Braveheart.jpg"
                },
                new Movie
                {
                    Title = "Avatar", ReleaseDate = DateTime.Parse("2009-03-22"),
                    Runtime = 165,
                    Description = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following " +
                    "his orders and protecting the world he feels is his home.",
                    DirectorId = 6,
                    ImageUrl = "21e9e20f-987e-4754-8e50-b405631f3030_Avatar.jpg"
                },
                new Movie
                {
                    Title = "Joker", ReleaseDate = DateTime.Parse("2019-10-05"),
                    Runtime = 162,
                    Description = "In Gotham City, mentally-troubled comedian Arthur Fleck is disregarded and mistreated by society. " +
                    "He then embarks on a downward spiral of revolution and bloody crime. This path brings him face-to-face with his " +
                    "alter-ego: The Joker.", 
                    DirectorId = 5,
                    ImageUrl = "9a2f1765-c724-41dd-a341-f3c1cd29557e_Joker.jpg"
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
                    "learn the world is in ruins, and must lead a group of survivors to stay alive.",
                    ImageUrl = "fb9da81a-08db-4030-8442-3b0a0bbbd94b_TheWalkingDead.jpg"
                },
                new TVShow
                {
                    Title = "Breaking Bad", ReleaseDate = DateTime.Parse("2008-01-01"),
                    NumberOfEpisode = 62,
                    Description = "A high school chemistry teacher diagnosed with inoperable lung cancer " +
                    "turns to manufacturing and selling methamphetamine in order to secure his family's future.",
                    ImageUrl = "b0e32fa6-c7e9-4dc5-b5cc-35bf341233fa_BreakingBad.jpg"
                },
                new TVShow
                {
                    Title = "Game of Thrones", ReleaseDate = DateTime.Parse("2011-10-15"),
                    NumberOfEpisode = 73,
                    Description = "Nine noble families fight for control over the mythical lands of Westeros, " +
                    "while an ancient enemy returns after being dormant for thousands of years.",
                    ImageUrl = "d1a5867d-07c2-413a-9e9f-ec6b4ca84f8c_GameOfThrones.jpeg"
                },
                new TVShow
                {
                    Title = "La Casa de Papel", ReleaseDate = DateTime.Parse("2017-12-11"),
                    NumberOfEpisode = 26,
                    Description = "A group of unique robbers assault the Factory of Moneda and Timbre to carry out " +
                    "the most perfect robbery in the history of Spain and take home 2.4 billion euros.",
                    ImageUrl = "a71c63bd-d527-410f-a5fe-27c7ebc38d0a_LaCasaDePapel.jpg"
                },
                new TVShow
                {
                    Title = "Peaky Blinders", ReleaseDate = DateTime.Parse("2013-03-22"),
                    NumberOfEpisode = 37,
                    Description = "A gangster family epic set in 1919 Birmingham, England; centered on a gang who sew razor " +
                    "blades in the peaks of their caps, and their fierce boss Tommy Shelby.",
                    ImageUrl = "d54b652e-905e-44ac-9889-3b84090c984d_PeakyBlinders.jpg"
                },
                new TVShow
                {
                    Title = "The Haunting of Hill House", ReleaseDate = DateTime.Parse("2018-10-05"),
                    NumberOfEpisode = 10,
                    Description = "Flashing between past and present, a fractured family confronts haunting memories of their " +
                    "old home and the terrifying events that drove them from it.",
                    ImageUrl = "48970eb2-4bbe-47ae-b5ac-87164614a8e1_TheHauntingOfHillHouse.jpg"
                },
                new TVShow
                {
                    Title = "Vikings", ReleaseDate = DateTime.Parse("2013-10-05"),
                    NumberOfEpisode = 89,
                    Description = "Vikings transports us to the brutal and mysterious world of Ragnar Lothbrok, a Viking warrior and " +
                    "farmer who yearns to explore - and raid - the distant shores across the ocean.",
                    ImageUrl = "5d8ff2ca-e44c-4009-9498-150944e30dc9_Vikings.png"
                },
                new TVShow
                {
                    Title = "The Wire", ReleaseDate = DateTime.Parse("2002-10-05"),
                    NumberOfEpisode = 60,
                    Description = "The Baltimore drug scene, as seen through the eyes of drug dealers and law enforcement.",
                    ImageUrl = "987f8bb7-b3e3-4a22-9409-30a310326827_TheWire.jpg"
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
                    Description = "Liam Neeson was born on June 7, 1952 in Ballymena, Northern Ireland, to Katherine (Brown), " +
                    "a cook, and Bernard Neeson, a school caretaker. He was raised in a Catholic household. During his early years, " +
                    "Liam worked as a forklift operator for Guinness, a truck driver, an assistant architect and an amateur boxer. " +
                    "He had originally sought a career as a teacher by attending St. Mary's Teaching College, Newcastle. ",
                    ImageUrl = "d0bf6057-edbe-437b-bd93-a7bed861a3d7_LiamNeeson.jpg"
                },
                new Star
                {
                    FirstName = "Tim", LastName = "Robbins", Birth = DateTime.Parse("1958-10-25"),
                    Description = "Born in West Covina, California, but raised in New York City, Tim Robbins is the son of former " +
                    "The Highwaymen singer Gil Robbins and actress Mary Robbins (née Bledsoe). Robbins studied drama at UCLA, " +
                    "where he graduated with honors in 1981. That same year, he formed the Actors' Gang theater group, an experimental " +
                    "ensemble that expressed radical political observations through the European avant-garde form of theater.",
                    ImageUrl = "39590549-7d7e-4a4a-b614-2106cf152727_TimRobbins.jpg"
                },
                new Star
                {
                    FirstName = "Morgan", LastName = "Freeman", Birth = DateTime.Parse("1937-07-01"),
                    Description = "With an authoritative voice and calm demeanor, this ever popular American actor has grown into " +
                    "one of the most respected figures in modern US cinema. Morgan was born on June 1, 1937 in Memphis, Tennessee, " +
                    "to Mayme Edna (Revere), a teacher, and Morgan Porterfield Freeman, a barber. The young Freeman attended Los Angeles " +
                    "City College before serving several years in the US Air Force as a mechanic between 1955 and 1959. His first " +
                    "dramatic arts exposure was on the stage including appearing in an all-African American production of the " +
                    "exuberant musical Hello, Dolly!.",
                    ImageUrl = "6b8a16e5-7efc-4caf-9a53-ee44a2ac729a_MorganFreeman.jpg"
                },
                new Star
                {
                    FirstName = "Russell", LastName = "Crowe", Birth = DateTime.Parse("1964-04-12"),
                    Description = "Russell Ira Crowe was born in Wellington, New Zealand, to Jocelyn Yvonne (Wemyss) and John Alexander " +
                    "Crowe, both of whom catered movie sets. His maternal grandfather, Stanley Wemyss, was a cinematographer. Crowe's " +
                    "recent ancestry includes Welsh (where his paternal grandfather was born, in Wrexham), English, Irish, Scottish, " +
                    "Norwegian, Swedish, and Maori (one of Crowe's maternal great-grandmothers, Erana Putiputi Hayes Heihi, was Maori).",
                    ImageUrl = "464987e6-4dcf-4d17-9842-2d1640f058c7_RussellCrowe.jpg"
                },
                new Star
                {
                    FirstName = "Joaquin", LastName = "Phoenix", Birth = DateTime.Parse("1974-10-28"),
                    Description = "Joaquin Phoenix was born Joaquin Rafael Bottom in San Juan, Puerto Rico, to Arlyn (Dunetz) " +
                    "and John Bottom, and is the middle child in a brood of five. His parents, from the continental United States, " +
                    "were then serving as Children of God missionaries. His mother is from a Jewish family from New York, while his " +
                    "father, from California, is of mostly British Isles descent. As a youngster, Joaquin took his cues from older " +
                    "siblings River Phoenix and Rain Phoenix, changing his name to Leaf to match their earthier monikers. When the " +
                    "children were encouraged to develop their creative instincts, he followed their lead into acting. Younger sisters " +
                    "Liberty Phoenix and Summer Phoenix rounded out the talented troupe.",
                    ImageUrl = "ec96eef5-7f63-4c28-9add-5543b4c610c0_JoaquinPhoenix.jpg"
                },
                new Star
                {
                    FirstName = "Mel", LastName = "Gibson", Birth = DateTime.Parse("1956-01-07"),
                    Description = "Mel Columcille Gerard Gibson was born January 3, 1956 in Peekskill, New York, USA, " +
                    "as the sixth of eleven children of Hutton Gibson, a railroad brakeman, and Anne Patricia (Reilly) " +
                    "Gibson (who died in December of 1990). His mother was Irish, from County Longford, while his American-born " +
                    "father is of mostly Irish descent.",
                    ImageUrl = "f1c29ef0-1699-4455-a1bb-34577e9d1d7e_MelGibson.jpg"
                },
                new Star
                {
                    FirstName = "Sam", LastName = "Worthington", Birth = DateTime.Parse("1976-08-19"),
                    Description = "Samuel Henry John Worthington was born August 2, 1976 in Surrey, England. His parents, " +
                    "Jeanne (Martyn) and Ronald Worthington, a power plant employee, moved the family to Australia when he was " +
                    "six months old, and raised him and his sister Lucinda in Warnbro, a suburb of Perth, Western Australia.",
                    ImageUrl = "4d1b63c9-e54e-4abc-a097-b971e6a1e096_SamWorthington.jpg"
                },
                new Star
                {
                    FirstName = "Andrew", LastName = "Lincoln", Birth = DateTime.Parse("1973-02-10"),
                    Description = "Andrew Lincoln is a British actor. Lincoln spent his early childhood in Hull, Yorkshire before " +
                    "his family relocated to Bath, Somerset when he was age 10. He was educated at Beechen Cliff School in Bath, " +
                    "and then the prestigious Royal Academy of Dramatic Art in London.",
                    ImageUrl = "1c2596e2-eca9-4219-a300-5f2ec80d7007_AndrewLincoln.jpg"
                },
                new Star
                {
                    FirstName = "Norman", LastName = "Reedus", Birth = DateTime.Parse("1969-01-09"),
                    Description = "Norman Reedus was born in Hollywood, Florida, to Marianne and Norman Reedus. He is of Italian " +
                    "(from his paternal grandmother), English, Scottish, and Irish descent. Norman's first film was in 'Guillermo " +
                    "Del Toro''s 1997 horror thriller Mimic (1997), where he played the character Jeremy.",
                    ImageUrl = "35f585fc-22e2-4258-b78f-1c335066a58a_NormanReedus.jpg"
                },
                new Star
                {
                    FirstName = "Bryan", LastName = "Cranston", Birth = DateTime.Parse("1956-02-10"),
                    Description = "Bryan Lee Cranston was born on March 7, 1956 in Hollywood, California, to Audrey Peggy Sell, " +
                    "a radio actress, and Joe Cranston, an actor and former amateur boxer. His maternal grandparents were German, " +
                    "and his father was of Irish, German, and Austrian-Jewish ancestry.",
                    ImageUrl = "d80501ca-414d-4949-893a-bdb2c62ff451_BryanCranston.jpg"
                },
                new Star
                {
                    FirstName = "Aaron", LastName = "Paul", Birth = DateTime.Parse("1979-10-22"),
                    Description = "Aaron Paul was born Aaron Paul Sturtevant in Emmett, Idaho, to Darla (Haynes) and Robert Sturtevant, " +
                    "a retired Baptist minister. While growing up, Paul took part in church programs, and performed in plays. " +
                    "He attended Centennial High School in Boise, Idaho. It was there, in eighth grade, that Aaron decided he " +
                    "wanted to become an actor.",
                    ImageUrl = "b14f4897-5ccb-4ea0-9622-67df881e3009_AaronPaul.jpg"
                },
                new Star
                {
                    FirstName = "Emilia", LastName = "Clarke", Birth = DateTime.Parse("1986-10-11"),
                    Description = "British actress Emilia Clarke was born in London and grew up in Oxfordshire, England. " +
                    "Her father was a theatre sound engineer and her mother is a businesswoman. Her father was working on a " +
                    "theatre production of Show Boat and her mother took her along to the performance.",
                    ImageUrl = "38da2237-1972-4249-8e90-3886bfe36a60_EmiliaClarke.jpg"
                },
                new Star
                {
                    FirstName = "Peter", LastName = "Dinklage", Birth = DateTime.Parse("1969-10-23"),
                    Description = "Peter Dinklage is an American actor. Since his breakout role in The Station Agent (2003), " +
                    "he has appeared in numerous films and theatre plays. Since 2011, Dinklage has portrayed Tyrion Lannister " +
                    "in the HBO series Game of Thrones. For this he won an Emmy for Outstanding Supporting Actor in a Drama Series " +
                    "and a Golden Globe Award for Best Supporting Actor - Series, Miniseries or Television Film in 2011.",
                    ImageUrl = "a4b0d624-5778-4aa8-a915-07e6b5f68246_PeterDinklage.jpg"
                },
                new Star
                {
                    FirstName = "Kit", LastName = "Harington", Birth = DateTime.Parse("1986-12-08"),
                    Description = "Kit Harington was born Christopher Catesby Harington in Acton, London, to Deborah Jane " +
                    "(Catesby), a former playwright, and David Richard Harington, a businessman. His mother named him after " +
                    "16th century British playwright and poet Christopher Marlowe, whose first name was shortened to Kit, a " +
                    "name Harington prefers. Harington's uncle is Sir Nicholas John Harington, the 14th Baronet Harington, and " +
                    "his paternal great-grandfather was Sir Richard Harington, the 12th Baronet Harington.",
                    ImageUrl = "43fc9aab-c82f-4da5-b6de-8964d1cc61c8_KitHarington.jpg"
                },
                new Star
                {
                    FirstName = "Úrsula", LastName = "Corberó", Birth = DateTime.Parse("1989-11-08"),
                    Description = "Úrsula Corberó Delgado (born 11 August 1989) is a Spanish actress, best known for her roles as " +
                    "Ruth in the Antena 3 series Física o química (2008), Margarita de Austria en Isabel (2011), Esther Salinas " +
                    "in the series La embajada and Tokyo in the television series La Casa de Papel (2017).",
                    ImageUrl = "f9347ad1-458a-4fae-82fb-0a9cfcb7c55a_ÚrsulaCorberó.jpg"
                },
                new Star
                {
                    FirstName = "Cillian", LastName = "Murphy", Birth = DateTime.Parse("1976-10-23"),
                    Description = "Striking Irish actor Cillian Murphy was born in Douglas, the oldest child of Brendan Murphy, " +
                    "who works for the Irish Department of Education, and a mother who is a teacher of French. He has three " +
                    "younger siblings. Murphy was educated at Presentation Brothers College, Cork.",
                    ImageUrl = "019fdffa-d4dd-4843-a57b-667150c79f98_CillianMurphy.jpg"
                },
                new Star
                {
                    FirstName = "Henry", LastName = "Thomas", Birth = DateTime.Parse("1971-07-09"),
                    Description = "Henry Thomas was born on September 9, 1971 in San Antonio, Texas, USA as Henry Jackson Thomas Jr. " +
                    "He is an actor and producer, known for E.T. the Extra-Terrestrial (1982), 11:14 (2003) and Legends of " +
                    "the Fall (1994). He was previously married to Marie Zielcke and Kelly Hill.",
                    ImageUrl = "2126a07f-a07f-4481-8ecb-ea76316ada7b_HenryThomas.jpg"
                },
                new Star
                {
                    FirstName = "Gustaf", LastName = "Skarsgård", Birth = DateTime.Parse("1980-11-12"),
                    Description = "Gustaf Skarsgård was born on November 12, 1980 in Stockholm, Stockholms län, Sweden as Gustaf " +
                    "Caspar Orm Skarsgård. He is an actor and director, known for Vikings (2013), Westworld (2016) and The Way " +
                    "Back (2010).",
                    ImageUrl = "57665492-65a4-438a-b9c7-19c7440e29d0_GustafSkarsgård.jpg"
                },
                new Star
                {
                    FirstName = "Katheryn", LastName = "Winnick", Birth = DateTime.Parse("1977-12-17"),
                    Description = "Canadian actress, director and producer Katheryn Winnick stars in the critically acclaimed, " +
                    "Emmy award-winning television series Vikings, produced by MGM and The History Channel. In addition to her " +
                    "lead role on Vikings, an episode directed by Winnick will debut in 2019.",
                    ImageUrl = "6d071dc3-6b54-4d4a-ba09-b93313a59332_KatherynWinnick.jpg"
                },
                new Star
                {
                    FirstName = "Alexander", LastName = "Ludwig", Birth = DateTime.Parse("1992-07-07"),
                    Description = "Alexander Richard Ludwig was born in Vancouver, Canada. After Starring in the hugely successful " +
                    "blockbuster, The Hunger Games (2012) (for which he received two awards), Alexander went on to work in films " +
                    "such as Lone Survivor (2013) and The Final Girls (2015) and received critical acclaim and for his performance " +
                    "as Bjorn Ironside in the Global Hit Television Series,Vikings (2013).",
                    ImageUrl = "e239d3c7-d84d-4cf9-8bc0-40c10c8cc0c3_AlexanderLudwig.jpg"
                },
                new Star
                {
                    FirstName = "Dominic", LastName = "West", Birth = DateTime.Parse("1969-10-15"),
                    Description = "Dominic West was born on October 15, 1969 in Sheffield, Yorkshire, England as Dominic Gerard " +
                    "Francis Eagleton West. He is an actor and producer, known for The Wire (2002), Chicago (2002) and Punisher: " +
                    "War Zone (2008). He has been married to Catherine Fitzgerald since June 26, 2010. ",
                    ImageUrl = "2d77fae9-ef5d-418e-a370-6bfbd10f2903_DominicWest.jpg"
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
