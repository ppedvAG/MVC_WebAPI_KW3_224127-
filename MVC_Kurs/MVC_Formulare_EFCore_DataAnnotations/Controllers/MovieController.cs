using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MVC_Formulare_EFCore_DataAnnotations.Data;
using MVC_Formulare_EFCore_DataAnnotations.Models;

namespace MVC_Formulare_EFCore_DataAnnotations.Controllers
{
    public class MovieController : Controller
    {
        private MovieDbContext _context;

        public MovieController(MovieDbContext movieDbContext)
        {
            _context = movieDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            //ToListAsync kommt auf dem EFCore - Package!
            return View(await _context.Movies.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Movie currentMovie = await _context.Movies.FirstOrDefaultAsync(e => e.Id == id);
            
            if (currentMovie == null)
            {
                return NotFound("Id nicht gefunden"); //404 Fehler ausgeben (ID ist falsch) 
            }

            return View(currentMovie);
        }

        [HttpGet]
        public async Task<IActionResult> Test(int id, string name)
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create (Movie movie)
        {

            //Wir können in Kompination zu mehreren Felder eine Validierung aufbauen
            
            if (movie.Title == "The Crow" && movie.ReleaseDate.Year == 1981)
            {
                //ModelState.IsValid wird auf false gesetzt 
                ModelState.AddModelError("Title", $"Der Film {movie.Title} aus dem Jahr {movie.ReleaseDate.Year} steht auf dem Index");
                ModelState.AddModelError("ReleaseDate", $"Der Film {movie.Title} aus dem Jahr {movie.ReleaseDate.Year} steht auf dem Index");
            }
       

            if (!ModelState.IsValid)
            {
                return View(movie); //Aktuelle Movie-Eingabe wird an das Create-Formular zurück übertragen
            }

            _context.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id = 123 });

        }
    }
}
