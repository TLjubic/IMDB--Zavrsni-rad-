using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Model;
using Microsoft.AspNetCore.Hosting;
using Kino.Models.Kino;
using Kino.Models.Kino.StarModel;
using System.IO;

namespace Kino.Controllers
{
    public class StarController : Controller
    {
        public string errorMessage = "Star is not available!";

        private readonly KinoDb _context;
        private IHostingEnvironment _hostingEnvironment;

        public StarController(KinoDb context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Star
        public async Task<IActionResult> Index(string sortOrder)
        {
            var viewModel = new StarViewModel();

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            viewModel.Stars = await _context.Stars
                .Include(s => s.ListOfMovies)
                    .ThenInclude(s => s.Movie)
                .Include(s => s.ListOfTVShows)
                    .ThenInclude(s => s.TVShow)
                .AsNoTracking()
                .ToListAsync();

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Stars = viewModel.Stars.OrderByDescending(s => s.FullName);
                    break;
                case "Date":
                    viewModel.Stars = viewModel.Stars.OrderBy(s => s.Birth);
                    break;
                case "date_desc":
                    viewModel.Stars = viewModel.Stars.OrderByDescending(s => s.Birth);
                    break;
                default:
                    viewModel.Stars = viewModel.Stars.OrderBy(s => s.FullName);
                    break;
            }

            MenuItems(viewModel);

            return View(viewModel);
        }

        // GET: Star/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var viewModel = new StarViewModel();

                viewModel.Star = await _context.Stars
                    .Include(s => s.ListOfMovies)
                        .ThenInclude(s => s.Movie)
                            .ThenInclude(s => s.ListOfGenres)
                                .ThenInclude(s => s.Genre)
                    .Include(s => s.ListOfTVShows)
                        .ThenInclude(s => s.TVShow)
                            .ThenInclude(s => s.ListOfGenres)
                                .ThenInclude(s => s.Genre)
                    .FirstOrDefaultAsync(m => m.StarId == id);

                if (viewModel.Star == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }

                MenuItems(viewModel);

                return View(viewModel);
            }

            TempData["Message"] = errorMessage;
            return RedirectToAction(nameof(Index));

        }

        // GET: Star/Create
        public IActionResult Create()
        {
            var viewModel = new StarViewModel();

            MenuItems(viewModel);

            return View(viewModel);
        }

        // POST: Star/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StarViewModel starViewModel)
        {
            
            if (ModelState.IsValid)
            {

                Star star = starViewModel.Star;

                if (starViewModel.Multimedia.Image != null)
                {
                    AddImage(star, starViewModel);
                }

                _context.Add(star);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            MenuItems(starViewModel);

            return View(starViewModel);
        }

        // GET: Star/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var viewModel = new StarViewModel();

                MenuItems(viewModel);

                viewModel.Star = await _context.Stars.FindAsync(id);
                if (viewModel.Star == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }
                return View(viewModel);
            }

            TempData["Message"] = errorMessage;
            return RedirectToAction(nameof(Index));

        }

        // POST: Star/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StarViewModel starViewModel)
        {
            if (id != starViewModel.Star.StarId)
            {
                TempData["Message"] = errorMessage;
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {

                Star star = starViewModel.Star;

                if (starViewModel.Multimedia.Image != null)
                {
                    AddImage(star, starViewModel);
                }

                try
                {
                    _context.Update(star);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarExists(star.StarId))
                    {
                        TempData["Message"] = errorMessage;
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Star", new { id = star.StarId });
            }

            MenuItems(starViewModel);

            return View(starViewModel);
        }

        // GET: Star/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var star = await _context.Stars
                .FirstOrDefaultAsync(m => m.StarId == id);
                if (star == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }

                return View(star);
            }

            TempData["Message"] = errorMessage;
            return RedirectToAction(nameof(Index));

        }

        // POST: Star/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var star = await _context.Stars.FindAsync(id);
            _context.Stars.Remove(star);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarExists(int id)
        {
            return _context.Stars.Any(e => e.StarId == id);
        }


        // Add Image
        public void AddImage (Star star, StarViewModel starViewModel)
        {
            star.ImageUrl = starViewModel.Multimedia.AddingImageUrl(starViewModel.Multimedia.Image, _hostingEnvironment);
        }


        // Fill menu items
        public void MenuItems (StarViewModel viewModel)
        {
            ViewModel model = new ViewModel();
            model = model.fillMenuItems(_context, model);
            viewModel.ListOfMovies = model.ListOfMovies;
            viewModel.ListOfTVShows = model.ListOfTVShows;
            viewModel.ListOfStars = model.ListOfStars;
            viewModel.ListOfDirectors = model.ListOfDirectors;
        }

    }
}
