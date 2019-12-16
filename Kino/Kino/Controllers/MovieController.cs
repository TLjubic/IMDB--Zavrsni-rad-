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
using Kino.Models.Kino.MovieModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Kino.Controllers
{
    public class MovieController : Controller
    {
        public string errorMessage = "Movie is not available!";

        private readonly KinoDb _context;
        private UserManager<AppUser> _userManager;
        private IHostingEnvironment _hostingEnvironment;

        public MovieController(KinoDb context, UserManager<AppUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
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

            ViewModel model = new ViewModel();
            model = model.fillMenuItems(_context, model);
            viewModel.ListOfMovies = model.ListOfMovies;
            viewModel.ListOfTVShows = model.ListOfTVShows;
            viewModel.ListOfStars = model.ListOfStars;
            viewModel.ListOfDirectors = model.ListOfDirectors;


            return View(viewModel);
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var viewModel = new ReviewDetailMovieModel();

                viewModel.Movie = await _context.Movies
                    .Include(i => i.ListOfGenres)
                        .ThenInclude(i => i.Genre)
                    .Include(i => i.ListOfStars)
                        .ThenInclude(i => i.Star)
                    .Include(i => i.Director)
                    .Include(i => i.ListOfReviews)
                    .FirstOrDefaultAsync(m => m.MovieId == id);

                if (viewModel.Movie == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }

                ViewModel model = new ViewModel();
                model = model.fillMenuItems(_context, model);
                viewModel.ListOfMovies = model.ListOfMovies;
                viewModel.ListOfTVShows = model.ListOfTVShows;
                viewModel.ListOfStars = model.ListOfStars;
                viewModel.ListOfDirectors = model.ListOfDirectors;

                return View(viewModel);
            }

            TempData["Message"] = errorMessage;
            return RedirectToAction(nameof(Index));

        }

        // GET: Movie/Search/Drama
        public IActionResult Search(string id, string sortOrder)
        {

            var viewModel = new MovieIndexModel();

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (id == null)
            {
                viewModel.Movies = _context.Movies
                .Include(g => g.ListOfGenres)
                    .ThenInclude(g => g.Genre)
                .Include(s => s.ListOfStars)
                    .ThenInclude(s => s.Star)
                .Include(i => i.Director)
                .AsNoTracking()
                .ToList();
            }
            else
            {
                viewModel.Movies = _context.Movies
                .Include(g => g.ListOfGenres)
                    .ThenInclude(g => g.Genre)
                .Include(s => s.ListOfStars)
                    .ThenInclude(s => s.Star)
                .Include(i => i.Director)
                .Where(i => i.ListOfGenres
                    .Any(g => g.Genre.Name.Contains(id)))
                .ToList();
            }
            

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Movies = viewModel.Movies.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    viewModel.Movies = viewModel.Movies.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    viewModel.Movies = viewModel.Movies.OrderByDescending(s => s.ReleaseDate);
                    break;
                default:
                    viewModel.Movies = viewModel.Movies.OrderBy(s => s.Title);
                    break;
            }

            ViewModel model = new ViewModel();
            model = model.fillMenuItems(_context, model);
            viewModel.ListOfMovies = model.ListOfMovies;
            viewModel.ListOfTVShows = model.ListOfTVShows;
            viewModel.ListOfStars = model.ListOfStars;
            viewModel.ListOfDirectors = model.ListOfDirectors;

            return View("Index", viewModel);

        }

        // POST: Movie/Details/5
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReview(ReviewDetailMovieModel reviewDetailModel)
        {

            if (reviewDetailModel.MovieReview.Body != null && reviewDetailModel.MovieReview.Title != null)
            {
                var userId = this._userManager.GetUserId(base.User);
                var userName = this._userManager.GetUserName(base.User);

                MovieReview movieReview = reviewDetailModel.MovieReview;
                movieReview.MovieId = reviewDetailModel.Movie.MovieId;
                movieReview.Date = DateTime.Now;
                movieReview.Username = userName;

                _context.Add(movieReview);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Comment successfully added.";
                return RedirectToAction("Details", "Movie", new { id = movieReview.MovieId });
            }

            return RedirectToAction("Details", "Movie", new { id = reviewDetailModel.MovieReview.MovieId });
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            FillGenresDataNew();
            FillDropdown();

            var viewModel = new MovieStarModel();
            ViewModel model = new ViewModel();
            model = model.fillMenuItems(_context, model);
            viewModel.ListOfMovies = model.ListOfMovies;
            viewModel.ListOfTVShows = model.ListOfTVShows;
            viewModel.ListOfStars = model.ListOfStars;
            viewModel.ListOfDirectors = model.ListOfDirectors;

            return View(viewModel);
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string[] selectedGenres, MovieStarModel movieStarModel)
        {
            Movie movie = movieStarModel.Movie;

            if (ModelState.IsValid)
            {

                if (movieStarModel.Multimedia.Image != null)
                {
                    movie.ImageUrl = movieStarModel.Multimedia.AddingImageUrl(movieStarModel.Multimedia.Image, _hostingEnvironment);
                }

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
            if (id != null)
            {
                var viewModel = new MovieEditModel();

                viewModel.Movie = await _context.Movies
                    .Include(i => i.ListOfGenres)
                        .ThenInclude(i => i.Genre)
                    .Include(i => i.ListOfStars)
                        .ThenInclude(i => i.Star)
                    .Include(i => i.Director)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.MovieId == id);

                if (viewModel.Movie == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }

                FillGenresData(viewModel.Movie);
                this.FillDropdown();

                ViewModel model = new ViewModel();
                model = model.fillMenuItems(_context, model);
                viewModel.ListOfMovies = model.ListOfMovies;
                viewModel.ListOfTVShows = model.ListOfTVShows;
                viewModel.ListOfStars = model.ListOfStars;
                viewModel.ListOfDirectors = model.ListOfDirectors;

                return View(viewModel);
            }

            TempData["Message"] = errorMessage;
            return RedirectToAction(nameof(Index));
        }


        
        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, MovieEditModel movieEditModel, string[] selectedGenres, int[] selectedStars)
        {
            if (id != null)
            {
                var viewModel = new MovieEditModel();

                Movie movie = movieEditModel.Movie;

                _context.Update(movie);
                await _context.SaveChangesAsync();

                movie = await _context.Movies
                    .Include(i => i.ListOfGenres)
                        .ThenInclude(i => i.Genre)
                    .Include(i => i.ListOfStars)
                        .ThenInclude(i => i.Star)
                    .Include(i => i.Director)
                    .FirstOrDefaultAsync(m => m.MovieId == id);

                if (movie == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }

                if (await TryUpdateModelAsync<Movie>(movie))
                {
                    UpdateMovie(selectedGenres, selectedStars, movie);

                    if (movieEditModel.Multimedia.Image != null)
                    {
                        movie.ImageUrl = movieEditModel.Multimedia.AddingImageUrl(movieEditModel.Multimedia.Image, _hostingEnvironment);
                    }

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Details", "Movie", new { id = movie.MovieId });
                }


                UpdateMovie(selectedGenres, selectedStars, movie);
                FillGenresData(movie);
                this.FillDropdown();

                ViewModel model = new ViewModel();
                model = model.fillMenuItems(_context, model);
                viewModel.ListOfMovies = model.ListOfMovies;
                viewModel.ListOfTVShows = model.ListOfTVShows;
                viewModel.ListOfStars = model.ListOfStars;
                viewModel.ListOfDirectors = model.ListOfDirectors;

                return View(viewModel);
            }

            TempData["Message"] = errorMessage;
            return RedirectToAction(nameof(Index));

        }


        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var movie = await _context.Movies
                 .FirstOrDefaultAsync(m => m.MovieId == id);
                if (movie == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }

                return View(movie);
            }

            TempData["Message"] = errorMessage;
            return RedirectToAction(nameof(Index));


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
