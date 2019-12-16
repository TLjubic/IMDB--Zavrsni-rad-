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
using Kino.Models.Kino.TVShowModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Kino.Controllers
{
    public class TVShowController : Controller
    {
        public string errorMessage = "TV show is not available!";

        private readonly KinoDb _context;
        private UserManager<AppUser> _userManager;
        private IHostingEnvironment _hostingEnvironment;

        public TVShowController(KinoDb context, UserManager<AppUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        // GET: TVShow
        public async Task<IActionResult> Index(int? id)
        {
            var viewModel = new TVIndexModel();

            viewModel.TVShows = await _context.TVShows
                .Include(g => g.ListOfGenres)
                    .ThenInclude(g => g.Genre)
                .Include(s => s.ListOfStars)
                    .ThenInclude(s => s.Star)
                .AsNoTracking()
                .ToListAsync();

            if (id != null)
            {
                TVShow tvShow = viewModel.TVShows.Where(i => i.TVShowId == id.Value).Single();
                viewModel.Genres = tvShow.ListOfGenres.Select(i => i.Genre);
                viewModel.Stars = tvShow.ListOfStars.Select(i => i.Star);
            }

            ViewModel model = new ViewModel();
            model = model.fillMenuItems(_context, model);
            viewModel.ListOfMovies = model.ListOfMovies;
            viewModel.ListOfTVShows = model.ListOfTVShows;
            viewModel.ListOfStars = model.ListOfStars;
            viewModel.ListOfDirectors = model.ListOfDirectors;

            return View(viewModel);
        }

        // GET: TVShow/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var viewModel = new ReviewDetailTVShowModel();

                viewModel.TVShow = await _context.TVShows
                    .Include(i => i.ListOfGenres)
                        .ThenInclude(i => i.Genre)
                    .Include(i => i.ListOfStars)
                        .ThenInclude(i => i.Star)
                    .Include(i => i.ListOfReviews)
                    .FirstOrDefaultAsync(m => m.TVShowId == id);

                if (viewModel.TVShow == null)
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

        // GET: TVShow/Search/Drama
        public IActionResult Search(string id, string sortOrder)
        {

            var viewModel = new TVIndexModel();

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (id == null)
            {
                viewModel.TVShows = _context.TVShows
                .Include(g => g.ListOfGenres)
                    .ThenInclude(g => g.Genre)
                .Include(s => s.ListOfStars)
                    .ThenInclude(s => s.Star)
                .AsNoTracking()
                .ToList();
            }
            else
            {
                viewModel.TVShows = _context.TVShows
                .Include(g => g.ListOfGenres)
                    .ThenInclude(g => g.Genre)
                .Include(s => s.ListOfStars)
                    .ThenInclude(s => s.Star)
                .Where(i => i.ListOfGenres
                    .Any(g => g.Genre.Name.Contains(id)))
                .ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.TVShows = viewModel.TVShows.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    viewModel.TVShows = viewModel.TVShows.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    viewModel.TVShows = viewModel.TVShows.OrderByDescending(s => s.ReleaseDate);
                    break;
                default:
                    viewModel.TVShows = viewModel.TVShows.OrderBy(s => s.Title);
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

        // POST: TVShow/Details/5
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReview(ReviewDetailTVShowModel reviewDetailModel)
        {
            if (reviewDetailModel.TVShowReview.Body != null && reviewDetailModel.TVShowReview.Title != null)
            {
                var userId = this._userManager.GetUserId(base.User);
                var userName = this._userManager.GetUserName(base.User);

                TVShowReview tvShowReview = reviewDetailModel.TVShowReview;
                tvShowReview.TVShowId = reviewDetailModel.TVShow.TVShowId;
                tvShowReview.Date = DateTime.Now;
                tvShowReview.Username = userName;
                _context.Add(tvShowReview);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Comment successfully added.";
                return RedirectToAction("Details", "TVShow", new { id = tvShowReview.TVShowId });
            }

            return RedirectToAction("Details", "TVShow", new { id = reviewDetailModel.TVShowReview.TVShowId });
        }


        // GET: TVShow/Create
        public IActionResult Create()
        {
            FillGenresDataNew();
            FillDropdown();

            var viewModel = new TVShowStarModel();
            ViewModel model = new ViewModel();
            model = model.fillMenuItems(_context, model);
            viewModel.ListOfMovies = model.ListOfMovies;
            viewModel.ListOfTVShows = model.ListOfTVShows;
            viewModel.ListOfStars = model.ListOfStars;
            viewModel.ListOfDirectors = model.ListOfDirectors;

            return View(viewModel);
        }

        // POST: TVShow/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string[] selectedGenres, TVShowStarModel tvShowStarModel)
        {

            var viewModel = new TVShowStarModel();
            viewModel.TVShow = tvShowStarModel.TVShow;

            if (ModelState.IsValid)
            {

                if (tvShowStarModel.Multimedia.Image != null)
                {
                    viewModel.TVShow.ImageUrl = tvShowStarModel.Multimedia.AddingImageUrl(tvShowStarModel.Multimedia.Image, _hostingEnvironment);

                }

                _context.Add(viewModel.TVShow);
                await _context.SaveChangesAsync();


                //Add genres to tvShows
                if (selectedGenres == null)
                {
                    viewModel.TVShow.ListOfGenres = new List<TVShowGenre>();
                }

                var selectedGenresHS = new HashSet<string>(selectedGenres);

                foreach (var genre in _context.Genres)
                {
                    if (selectedGenresHS.Contains(genre.GenreId.ToString()))
                    {
                        _context.Add(new TVShowGenre
                        {
                            TVShowId = viewModel.TVShow.TVShowId,
                            GenreId = genre.GenreId
                        });

                    }
                }


                //Add stars to tvShows

                if (tvShowStarModel.selectedStars == null)
                {
                    viewModel.TVShow.ListOfStars = new List<TVShowStar>();
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                var stars = await _context.Stars.ToListAsync();

                int counter = 0;

                foreach (var selectedId in tvShowStarModel.selectedStars)
                {
                    foreach (var star in stars)
                    {
                        counter++;

                        if (selectedId == counter)
                        {
                            _context.Add(new TVShowStar
                            {
                                TVShowId = viewModel.TVShow.TVShowId,
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
            ViewModel model = new ViewModel();
            model = model.fillMenuItems(_context, model);
            viewModel.ListOfMovies = model.ListOfMovies;
            viewModel.ListOfTVShows = model.ListOfTVShows;
            viewModel.ListOfStars = model.ListOfStars;
            viewModel.ListOfDirectors = model.ListOfDirectors;

            return View(viewModel);
        }

        // GET: TVShow/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var viewModel = new TVShowStarModel();

                viewModel.TVShow = await _context.TVShows
                    .Include(t => t.ListOfGenres)
                        .ThenInclude(t => t.Genre)
                    .Include(t => t.ListOfStars)
                        .ThenInclude(t => t.Star)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.TVShowId == id);

                if (viewModel.TVShow == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }

                FillGenresData(viewModel.TVShow);
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

        // POST: TVShow/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TVShowStarModel model, string[] selectedGenres, int[] selectedStars)
        {
            if (id != null)
            {
                var viewModel = new TVShowStarModel();
                viewModel.TVShow = model.TVShow;

                _context.Update(viewModel.TVShow);
                await _context.SaveChangesAsync();

                viewModel.TVShow = await _context.TVShows
                    .Include(t => t.ListOfGenres)
                        .ThenInclude(t => t.Genre)
                    .Include(t => t.ListOfStars)
                        .ThenInclude(t => t.Star)
                    .FirstOrDefaultAsync(t => t.TVShowId == id);

                if (viewModel.TVShow == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }

                if (await TryUpdateModelAsync<TVShow>(viewModel.TVShow))
                {
                    UpdateTVShow(selectedGenres, selectedStars, viewModel.TVShow);

                    if (model.Multimedia.Image != null)
                    {
                        viewModel.TVShow.ImageUrl = model.Multimedia.AddingImageUrl(model.Multimedia.Image, _hostingEnvironment);

                    }

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Details", "TVShow", new { id = viewModel.TVShow.TVShowId });
                }

                UpdateTVShow(selectedGenres, selectedStars, viewModel.TVShow);
                FillGenresData(viewModel.TVShow);
                this.FillDropdown();

                ViewModel modelView = new ViewModel();
                modelView = modelView.fillMenuItems(_context, modelView);
                viewModel.ListOfMovies = model.ListOfMovies;
                viewModel.ListOfTVShows = model.ListOfTVShows;
                viewModel.ListOfStars = model.ListOfStars;
                viewModel.ListOfDirectors = model.ListOfDirectors;

                return View(viewModel);
            }

            TempData["Message"] = errorMessage;
            return RedirectToAction(nameof(Index));

        }

        // GET: TVShow/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var tVShow = await _context.TVShows
                 .FirstOrDefaultAsync(m => m.TVShowId == id);
                if (tVShow == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }

                return View(tVShow);
            }

            TempData["Message"] = errorMessage;
            return RedirectToAction(nameof(Index));

        }

        // POST: TVShow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tVShow = await _context.TVShows.FindAsync(id);
            _context.TVShows.Remove(tVShow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TVShowExists(int id)
        {
            return _context.TVShows.Any(e => e.TVShowId == id);
        }


        //Star and director values for dropdown 
        private void FillDropdown()
        {
            var stars = new List<SelectListItem>();

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

            ViewBag.PossibleStars = stars;
        }

        //Fill genres for checkboxes
        private void FillGenresData(TVShow tvShow)
        {
            var allGenres = _context.Genres;
            var tvShowGenres = new HashSet<int>(tvShow.ListOfGenres.Select(c => c.GenreId));
            var viewModel = new List<TVShowGenreModel>();

            foreach (var genre in allGenres)
            {
                viewModel.Add(new TVShowGenreModel
                {
                    GenreId = genre.GenreId,
                    Title = genre.Name,
                    Check = tvShowGenres.Contains(genre.GenreId)
                });
            }
            ViewData["GenresTVShow"] = viewModel;
        }

        //Fill genres for checkboxes
        private void FillGenresDataNew()
        {
            var allGenres = _context.Genres;
            var viewModel = new List<TVShowGenreModel>();

            foreach (var genre in allGenres)
            {
                viewModel.Add(new TVShowGenreModel
                {
                    GenreId = genre.GenreId,
                    Title = genre.Name,
                    Check = false
                });
            }
            ViewData["GenresTVShowNew"] = viewModel;
        }


        //Update TVShow
        private void UpdateTVShow(string[] selectedGenres, int[] selectedStars, TVShow tvShowToUpdate)
        {
            if (selectedGenres == null)
            {
                tvShowToUpdate.ListOfGenres = new List<TVShowGenre>();
                return;
            }
            if (selectedStars == null)
            {
                tvShowToUpdate.ListOfStars = new List<TVShowStar>();
                return;
            }

            var selectedGenresHS = new HashSet<string>(selectedGenres);
            var tvShowGenres = new HashSet<int>
                (tvShowToUpdate.ListOfGenres.Select(c => c.Genre.GenreId));

            var selectedStarsHS = new HashSet<int>(selectedStars);
            var tvShowStars = new HashSet<int>
                (tvShowToUpdate.ListOfStars.Select(c => c.Star.StarId));

            foreach (var genre in _context.Genres)
            {
                if (selectedGenresHS.Contains(genre.GenreId.ToString()))
                {
                    if (!tvShowGenres.Contains(genre.GenreId))
                    {
                        tvShowToUpdate.ListOfGenres.Add(new TVShowGenre
                        {
                            TVShowId = tvShowToUpdate.TVShowId,
                            GenreId = genre.GenreId
                        });
                    }
                }
                else
                {

                    if (tvShowGenres.Contains(genre.GenreId))
                    {
                        TVShowGenre genreToRemove = tvShowToUpdate.ListOfGenres.FirstOrDefault(i => i.GenreId == genre.GenreId);
                        _context.Remove(genreToRemove);
                    }
                }
            }

            foreach (var star in _context.Stars)
            {
                if (selectedStarsHS.Contains(star.StarId))
                {
                    if (!tvShowStars.Contains(star.StarId))
                    {
                        tvShowToUpdate.ListOfStars.Add(new TVShowStar
                        {
                            TVShowId = tvShowToUpdate.TVShowId,
                            StarId = star.StarId
                        });
                    }
                }
                else
                {

                    if (tvShowStars.Contains(star.StarId))
                    {
                        TVShowStar starToRemove = tvShowToUpdate.ListOfStars.FirstOrDefault(i => i.StarId == star.StarId);
                        _context.Remove(starToRemove);
                    }
                }
            }
        }



        public IEnumerable<Movie> fillMenuItems()
        {
            var movies = _context.Movies
                .ToList();

            return movies;

        }

    }
}
