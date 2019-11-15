using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Model;
using Kino.Models.Kino;

namespace Kino.Controllers
{
    public class MovieController : Controller
    {
        private readonly KinoDb _context;

        public MovieController(KinoDb context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index(int? id)
        {
            var viewModel = new MovieIndexModel();
            
            viewModel.Movies = await _context.Movies
                .Include(g => g.ListOfGenres)
                    .ThenInclude(g => g.Genre)
                .Include(s => s.ListOfStars)
                    .ThenInclude(s => s.Star)
                .Include(i => i.Director)
                .AsNoTracking()
                .ToListAsync();
            
            if (id != null)
            {
                Movie movie = viewModel.Movies.Where(i => i.MovieId == id.Value).Single();
                viewModel.Genres = movie.ListOfGenres.Select(i => i.Genre);
                viewModel.Stars = movie.ListOfStars.Select(i => i.Star);
            }

            return View(viewModel);
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(i => i.ListOfGenres)
                    .ThenInclude(i => i.Genre)
                .Include(i => i.ListOfStars)
                    .ThenInclude(i => i.Star)
                .Include(i => i.Director)
                .FirstOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            FillGenresDataNew();
            FillDropdown();
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,ReleaseDate,Runtime,Description")] 
            string[] selectedGenres, MovieStarModel movieStarModel)
        {
            Movie movie = movieStarModel.Movie;

            if (ModelState.IsValid)
            {

                _context.Add(movie);
                await _context.SaveChangesAsync();


                //Add genres to movies
                if (selectedGenres == null)
                {
                    movie.ListOfGenres = new List<MovieGenre>();
                }

                var selectedGenresHS = new HashSet<string>(selectedGenres);

                foreach (var genre in _context.Genres)
                {
                    if (selectedGenresHS.Contains(genre.GenreId.ToString()))
                    {
                        _context.Add(new MovieGenre
                        {
                            MovieId = movie.MovieId,
                            GenreId = genre.GenreId
                        });
                        
                    }
                }


                //Add stars to movies

                if (movieStarModel.selectedStars == null)
                {
                    movie.ListOfStars = new List<MovieStar>();
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                var stars = await _context.Stars.ToListAsync();

                int counter = 0;

                foreach (var selectedId in movieStarModel.selectedStars)
                {
                    foreach (var star in stars)
                    {
                        counter++;

                        if (selectedId == counter)
                        {
                            _context.Add(new MovieStar
                            {
                                MovieId = movie.MovieId,
                                StarId = selectedId
                            });
                        }
                    }

                    counter = 0;
                }


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            FillDropdown();
            return View(movie);
        }


        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(i => i.ListOfGenres)
                    .ThenInclude(i => i.Genre)
                .Include(i => i.ListOfStars)
                    .ThenInclude(i => i.Star)
                .Include(i => i.Director)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            FillGenresData(movie);
            this.FillDropdown();

            return View(movie);
        }

        
        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedGenres, int[] selectedStars)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieToUpdate = await _context.Movies
                .Include(i => i.ListOfGenres)
                    .ThenInclude(i => i.Genre)
                .Include(i => i.ListOfStars)
                    .ThenInclude(i => i.Star)
                .Include(i => i.Director)
                .FirstOrDefaultAsync(m => m.MovieId == id);

            if (await TryUpdateModelAsync<Movie>(movieToUpdate))
            {
                UpdateMovie(selectedGenres, selectedStars, movieToUpdate);

                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
    

            UpdateMovie(selectedGenres, selectedStars, movieToUpdate);
            FillGenresData(movieToUpdate);
            this.FillDropdown();

            return View(movieToUpdate);
        }


        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }

        //Star and director values for dropdown 
        private void FillDropdown()
        {
            var stars = new List<SelectListItem>();
            var directors = new List<SelectListItem>();

            //Dropdown for stars
            var listItem = new SelectListItem();
            listItem.Text = "- Stars -";
            listItem.Value = "";
            stars.Add(listItem);

            foreach (var star in _context.Stars)
            {
                stars.Add(new SelectListItem()
                {
                    Value = "" + star.StarId,
                    Text = star.FullName
                });
            }

            //Dropdown for directors
            var listItemDirector = new SelectListItem();
            listItemDirector.Text = "- Directors -";
            listItemDirector.Value = "";
            directors.Add(listItemDirector);

            foreach (var director in _context.Directors)
            {
                directors.Add(new SelectListItem()
                {
                    Value = "" + director.DirectorId,
                    Text = director.FullName
                });
            }

            ViewBag.PossibleStars = stars;
            ViewBag.PossibleDirectors = directors;
        }

        //Fill genres for checkboxes
        private void FillGenresData(Movie movie)
        {
            var allGenres = _context.Genres;
            var movieGenres = new HashSet<int>(movie.ListOfGenres.Select(c => c.GenreId));
            var viewModel = new List<MovieGenreModel>();

            foreach (var genre in allGenres)
            {
                viewModel.Add(new MovieGenreModel
                {
                    GenreId = genre.GenreId,
                    Title = genre.Name,
                    Check = movieGenres.Contains(genre.GenreId)
                });
            }
            ViewData["GenresMovie"] = viewModel;
        }

        //Fill genres for checkboxes
        private void FillGenresDataNew()
        {
            var allGenres = _context.Genres;
            var viewModel = new List<MovieGenreModel>();

            foreach (var genre in allGenres)
            {
                viewModel.Add(new MovieGenreModel
                {
                    GenreId = genre.GenreId,
                    Title = genre.Name,
                    Check = false
                });
            }
            ViewData["GenresMovieNew"] = viewModel;
        }


        //Update movie
        private void UpdateMovie(string[] selectedGenres, int[] selectedStars, Movie movieToUpdate)
        {
            if (selectedGenres == null)
            {
                movieToUpdate.ListOfGenres = new List<MovieGenre>();
                return;
            }
            if (selectedStars == null)
            {
                movieToUpdate.ListOfStars = new List<MovieStar>();
                return;
            }

            var selectedGenresHS = new HashSet<string>(selectedGenres);
            var movieGenres = new HashSet<int>
                (movieToUpdate.ListOfGenres.Select(c => c.Genre.GenreId));

            var selectedStarsHS = new HashSet<int>(selectedStars);
            var movieStars = new HashSet<int>
                (movieToUpdate.ListOfStars.Select(c => c.Star.StarId));

            foreach (var genre in _context.Genres)
            {
                if (selectedGenresHS.Contains(genre.GenreId.ToString()))
                {
                    if (!movieGenres.Contains(genre.GenreId))
                    {
                        movieToUpdate.ListOfGenres.Add(new MovieGenre
                        {
                            MovieId = movieToUpdate.MovieId,
                            GenreId = genre.GenreId
                        });
                    }
                }
                else
                {

                    if (movieGenres.Contains(genre.GenreId))
                    {
                        MovieGenre genreToRemove = movieToUpdate.ListOfGenres.FirstOrDefault(i => i.GenreId == genre.GenreId);
                        _context.Remove(genreToRemove);
                    }
                }
            }

            foreach (var star in _context.Stars)
            {
                if (selectedStarsHS.Contains(star.StarId))
                {
                    if (!movieStars.Contains(star.StarId))
                    {
                        movieToUpdate.ListOfStars.Add(new MovieStar
                        {
                            MovieId = movieToUpdate.MovieId,
                            StarId = star.StarId
                        });
                    }
                }
                else
                {

                    if (movieStars.Contains(star.StarId))
                    {
                        MovieStar starToRemove = movieToUpdate.ListOfStars.FirstOrDefault(i => i.StarId == star.StarId);
                        _context.Remove(starToRemove);
                    }
                }
            }
        }

    }
}
