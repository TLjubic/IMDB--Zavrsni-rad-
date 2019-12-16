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
using System.IO;
using Kino.Models.Kino.DirectorModel;

namespace Kino.Controllers
{
    public class DirectorController : Controller
    {
        public string errorMessage = "Director is not available!";

        private readonly KinoDb _context;
        private IHostingEnvironment _hostingEnvironment;

        public DirectorController(KinoDb context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Director
        public async Task<IActionResult> Index(string sortOrder)
        {
            var viewModel = new DirectorViewModel();

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            viewModel.Directors = await _context.Directors.ToListAsync();

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Directors = viewModel.Directors.OrderByDescending(s => s.FullName);
                    break;
                case "Date":
                    viewModel.Directors = viewModel.Directors.OrderBy(s => s.Birth);
                    break;
                case "date_desc":
                    viewModel.Directors = viewModel.Directors.OrderByDescending(s => s.Birth);
                    break;
                default:
                    viewModel.Directors = viewModel.Directors.OrderBy(s => s.FullName);
                    break;
            }


            MenuItems(viewModel);

            return View(viewModel);
        }

        // GET: Director/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {

                var viewModel = new DirectorViewModel();

                viewModel.Director = await _context.Directors
                    .Include(d => d.ListOfMovies)
                    .FirstOrDefaultAsync(m => m.DirectorId == id);

                if (viewModel.Director == null)
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


        // GET: Director/Create
        public IActionResult Create()
        {
            var viewModel = new DirectorViewModel();

            MenuItems(viewModel);

            return View(viewModel);
        }

        // POST: Director/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DirectorViewModel directorViewModel)
        {

            if (ModelState.IsValid)
            {

                Director director = directorViewModel.Director;

                if (directorViewModel.Multimedia.Image != null)
                {
                    AddImage(director, directorViewModel);
                }

                _context.Add(director);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            MenuItems(directorViewModel);

            return View(directorViewModel);
        }

        // GET: Director/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var viewModel = new DirectorViewModel();

                MenuItems(viewModel);

                viewModel.Director = await _context.Directors.FindAsync(id);
                if (viewModel.Director == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }
                return View(viewModel);
            }

            TempData["Message"] = errorMessage;
            return RedirectToAction(nameof(Index));

        }

        // POST: Director/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DirectorViewModel directorViewModel)
        {
            if (id != directorViewModel.Director.DirectorId)
            {
                TempData["Message"] = errorMessage;
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {

                Director director = directorViewModel.Director;

                if (directorViewModel.Multimedia.Image != null)
                {
                    AddImage(director, directorViewModel);
                }

                try
                {
                    _context.Update(director);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorExists(director.DirectorId))
                    {
                        TempData["Message"] = errorMessage;
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Director", new { id = director.DirectorId });
            }

            MenuItems(directorViewModel);

            return View(directorViewModel);
        }

        // GET: Director/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var director = await _context.Directors
                .FirstOrDefaultAsync(m => m.DirectorId == id);
                if (director == null)
                {
                    TempData["Message"] = errorMessage;
                    return RedirectToAction(nameof(Index));
                }

                return View(director);
            }

            TempData["Message"] = errorMessage;
            return RedirectToAction(nameof(Index));

        }

        // POST: Director/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectorExists(int id)
        {
            return _context.Directors.Any(e => e.DirectorId == id);
        }


        // Add Image
        public void AddImage(Director director, DirectorViewModel directorViewModel)
        {
            director.ImageUrl = directorViewModel.Multimedia.AddingImageUrl(directorViewModel.Multimedia.Image, _hostingEnvironment);
        }


        // Fill menu items
        public void MenuItems(DirectorViewModel viewModel)
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
