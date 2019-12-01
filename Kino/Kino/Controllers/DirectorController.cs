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
    public class DirectorController : Controller
    {
        private readonly KinoDb _context;
        private IHostingEnvironment _hostingEnvironment;

        public DirectorController(KinoDb context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Director
        public async Task<IActionResult> Index()
        {
            return View(await _context.Directors.ToListAsync());
        }

        // GET: Director/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directors
                .Include(d => d.ListOfMovies)
                .FirstOrDefaultAsync(m => m.DirectorId == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // GET: Director/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Director/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DirectorViewModel directorViewModel)
        {
            Director director = directorViewModel.Director;

            if (ModelState.IsValid)
            {

                string fileName = null;

                if (directorViewModel.Image != null)
                {
                    string uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    fileName = Guid.NewGuid().ToString() + "_" + directorViewModel.Image.FileName;
                    string filePath = Path.Combine(uploadFile, fileName);
                    directorViewModel.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    director.Img = fileName;
                }

                _context.Add(director);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(directorViewModel);
        }

        // GET: Director/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = new DirectorViewModel();

            viewModel.Director = await _context.Directors.FindAsync(id);
            if (viewModel.Director == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Director/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DirectorViewModel directorViewModel)
        {
            if (id != directorViewModel.Director.DirectorId)
            {
                return NotFound();
            }

            Director director = directorViewModel.Director;

            if (ModelState.IsValid)
            {

                string fileName = null;

                if (directorViewModel.Image != null)
                {
                    string uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    fileName = Guid.NewGuid().ToString() + "_" + directorViewModel.Image.FileName;
                    string filePath = Path.Combine(uploadFile, fileName);
                    directorViewModel.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    director.Img = fileName;
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
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(directorViewModel);
        }

        // GET: Director/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directors
                .FirstOrDefaultAsync(m => m.DirectorId == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
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
    }
}
