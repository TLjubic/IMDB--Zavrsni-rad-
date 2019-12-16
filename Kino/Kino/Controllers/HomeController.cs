using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kino.Models;
using DAL;
using Kino.Models.Kino;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Kino.Controllers
{
    public class HomeController : Controller
    {

        private readonly KinoDb _context;

        public HomeController(KinoDb context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var viewModel = new ViewModel();

            viewModel.ListOfMovies = await _context.Movies
                .AsNoTracking()
                .ToListAsync();

            viewModel.ListOfTVShows = await _context.TVShows
                .AsNoTracking()
                .ToListAsync();

            viewModel.ListOfStars = await _context.Stars
                .AsNoTracking()
                .ToListAsync();

            viewModel.ListOfDirectors = await _context.Directors
                .AsNoTracking()
                .ToListAsync();

            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void SetItemsToSearch()
        {

            var movies = _context.Movies
                .Include(g => g.ListOfGenres)
                    .ThenInclude(g => g.Genre)
                .Include(s => s.ListOfStars)
                    .ThenInclude(s => s.Star)
                .Include(i => i.Director);
                

            ViewBag.Result = movies;
        }
    }
}
