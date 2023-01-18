using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorAdvanced.Data;
using RazorAdvanced.Models;

namespace RazorAdvanced.ViewComponents
{
    public class MoviesWithLowestPriceViewComponent : ViewComponent
    {
        private MovieDbContext _context;

        public MoviesWithLowestPriceViewComponent(MovieDbContext context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync(int top)
        {
            IList<Movie> newestMovies = await _context.Movie.OrderBy(e=>e.Price).Take(top).ToListAsync();
            return View(newestMovies);
        }
    }
}
