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
    public class TVShowController : Controller
    {
        private readonly KinoDb _context;

        public TVShowController(KinoDb context)
        {
            _context = context;
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

            return View(viewModel);
        }

        // GET: TVShow/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tVShow = await _context.TVShows
                .Include(t => t.ListOfGenres)
                    .ThenInclude(t => t.Genre)
                .Include(t => t.ListOfStars)
                    .ThenInclude(t => t.Star)
                .FirstOrDefaultAsync(m => m.TVShowId == id);

            if (tVShow == null)
            {
                return NotFound();
            }

            return View(tVShow);
        }

        // GET: TVShow/Create
        public IActionResult Create()
        {
            FillGenresDataNew();
            FillDropdown();
            return View();
        }

        // POST: TVShow/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TVShowId,Title,ReleaseDate,NumberOfEpisode,Description")]
            string[] selectedGenres, TVShowStarModel tvShowStarModel)
        {

            TVShow tvShow = tvShowStarModel.TVShow;

            if (ModelState.IsValid)
            {

                _context.Add(tvShow);
                await _context.SaveChangesAsync();


                //Add genres to tvShows
                if (selectedGenres == null)
                {
                    tvShow.ListOfGenres = new List<TVShowGenre>();
                }

                var selectedGenresHS = new HashSet<string>(selectedGenres);

                foreach (var genre in _context.Genres)
                {
                    if (selectedGenresHS.Contains(genre.GenreId.ToString()))
                    {
                        _context.Add(new TVShowGenre
                        {
                            TVShowId = tvShow.TVShowId,
                            GenreId = genre.GenreId
                        });

                    }
                }


                //Add stars to tvShows

                if (tvShowStarModel.selectedStars == null)
                {
                    tvShow.ListOfStars = new List<TVShowStar>();
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
                                TVShowId = tvShow.TVShowId,
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
            return View(tvShow);
        }

        // GET: TVShow/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TVShows
                .Include(t => t.ListOfGenres)
                    .ThenInclude(t => t.Genre)
                .Include(t => t.ListOfStars)
                    .ThenInclude(t => t.Star)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TVShowId == id);
      
            if (tvShow == null)
            {
                return NotFound();
            }

            FillGenresData(tvShow);
            this.FillDropdown();

            return View(tvShow);
        }

        // POST: TVShow/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedGenres, int[] selectedStars)
        {
            if (id == null)
            {
                return NotFound();
            }


            var tvShowToUpdate = await _context.TVShows
                .Include(t => t.ListOfGenres)
                    .ThenInclude(t => t.Genre)
                .Include(t => t.ListOfStars)
                    .ThenInclude(t => t.Star)
                .FirstOrDefaultAsync(t => t.TVShowId == id);


            if (await TryUpdateModelAsync<TVShow>(tvShowToUpdate))
            {
                UpdateTVShow(selectedGenres, selectedStars, tvShowToUpdate);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            UpdateTVShow(selectedGenres, selectedStars, tvShowToUpdate);
            FillGenresData(tvShowToUpdate);
            this.FillDropdown();

            return View(tvShowToUpdate);
        }

        // GET: TVShow/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tVShow = await _context.TVShows
                .FirstOrDefaultAsync(m => m.TVShowId == id);
            if (tVShow == null)
            {
                return NotFound();
            }

            return View(tVShow);
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
    }
}
