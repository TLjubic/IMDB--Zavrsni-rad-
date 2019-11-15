using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Model;

namespace Kino.Controllers
{
    public class StarController : Controller
    {
        private readonly KinoDb _context;

        public StarController(KinoDb context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("StarId,FirstName,LastName,Birth,Description")] Star star)
        {
            if (ModelState.IsValid)
            {
                _context.Add(star);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(star);
        }

        // GET: Star/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var star = await _context.Stars.FindAsync(id);
            if (star == null)
            {
                return NotFound();
            }
            return View(star);
        }

        // POST: Star/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StarId,FirstName,LastName,Birth,Description")] Star star)
        {
            if (id != star.StarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return View(star);
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
