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

namespace Kino.Controllers
{
    public class StarController : Controller
    {
        private readonly KinoDb _context;
        private IHostingEnvironment _hostingEnvironment;

        public StarController(KinoDb context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Star
        public async Task<IActionResult> Index()
        {
            var stars = await _context.Stars
                .Include(s => s.ListOfMovies)
                    .ThenInclude(s => s.Movie)
                .Include(s => s.ListOfTVShows)
                    .ThenInclude(s => s.TVShow)
                .AsNoTracking()
                .ToListAsync();

            return View(stars);
        }

        // GET: Star/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var star = await _context.Stars
                .Include(s => s.ListOfMovies)
                    .ThenInclude(s => s.Movie)
                        .ThenInclude(s => s.ListOfGenres)
                            .ThenInclude(s => s.Genre)
                .Include(s => s.ListOfTVShows)
                    .ThenInclude(s => s.TVShow)
                        .ThenInclude(s => s.ListOfGenres)
                            .ThenInclude(s => s.Genre)
                .FirstOrDefaultAsync(m => m.StarId == id);

            if (star == null)
            {
                return NotFound();
            }

            return View(star);
        }

        // GET: Star/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Star/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StarViewModel starViewModel)
        {
            Star star = starViewModel.Star;

            if (ModelState.IsValid)
            {
                string fileName = null;

                if (starViewModel.Image != null)
                {
                    string uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    fileName = Guid.NewGuid().ToString() + "_" + starViewModel.Image.FileName;
                    string filePath = Path.Combine(uploadFile, fileName);
                    starViewModel.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    star.Img = fileName;
                }


                _context.Add(star);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(starViewModel);
        }

        // GET: Star/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = new StarViewModel();

            viewModel.Star = await _context.Stars.FindAsync(id);
            if (viewModel.Star == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Star/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StarViewModel starViewModel)
        {
            if (id != starViewModel.Star.StarId)
            {
                return NotFound();
            }

            Star star = starViewModel.Star;

            if (ModelState.IsValid)
            {
                string fileName = null;

                if (starViewModel.Image != null)
                {
                    string uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    fileName = Guid.NewGuid().ToString() + "_" + starViewModel.Image.FileName;
                    string filePath = Path.Combine(uploadFile, fileName);
                    starViewModel.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    star.Img = fileName;
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
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(starViewModel);
        }

        // GET: Star/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var star = await _context.Stars
                .FirstOrDefaultAsync(m => m.StarId == id);
            if (star == null)
            {
                return NotFound();
            }

            return View(star);
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
    }
}
