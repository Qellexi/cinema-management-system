using CinemaApp.Data;
using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CinemaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ������� �������: ������ ������ � �����������
        public async Task<IActionResult> Index(string searchString, string movieGenre)
        {
            var movies = _context.Movies.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
                movies = movies.Where(s => s.Title.Contains(searchString));

            if (!String.IsNullOrEmpty(movieGenre))
                movies = movies.Where(x => x.Genre == movieGenre);

            var allMovies = await movies.ToListAsync();

            var now = DateTime.Now;

            ViewBag.NewMovies = allMovies
                .Where(m => m.ReleaseDate <= now)
                .OrderByDescending(m => m.ReleaseDate)
                .Take(10)
                .ToList();

            ViewBag.ComingSoon = allMovies
                .Where(m => m.ReleaseDate > now)
                .OrderBy(m => m.ReleaseDate)
                .Take(10)
                .ToList();

            return View(allMovies);
        }



        // ������� ������� ������ (� ���������)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _context.Movies
                .Include(m => m.Sessions) // ����������� ������ ����� � �������
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) return NotFound();

            return View(movie);
        }
    }
}