using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebAPI_ControllerSamples.Data;
using WebAPI_ControllerSamples.Models;


//Variante 4
//[assembly: ApiConventionType(typeof(DefaultApiConventions))]

/*
     * [assembly: ApiConventionType(typeof(DefaultApiConventions))]
        namespace ApiConventions
        {
            public class Startup
            {
 * 
 * 
 */

namespace WebAPI_ControllerSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //Markiert, dass die Klasse sich um einen WebAPI-Controller handelt
    [Produces("application/xml")] //Wir begrenzen die Ausgabe dieses Controllers nur auf das XML-Format

    //Variante 3:
    [ApiConventionType(typeof(DefaultApiConventions))]


    public class ConventionsController : ControllerBase
    {
        private readonly MovieDbContext _context;

        public ConventionsController(MovieDbContext dbContext)
        {
            _context= dbContext;
        }


        /* Minimalste Konventionen bei Swagger und WebAPI
         * - Swagger:
         *  - benötigt eine [HttpGet] - Verb
         * 
         * - WebAPI:
         *  - erkennt eine Get/Post/Put/Delete Methode anhand des Prefixes des Methoden-Namens (was bei Swagger zu einem Fehler führt) 
         *  
         *  !!! Damit wir weiterhin im Kurs Swagger verwenden können, mussten wir in diesem Beispiel am Ende noch ein HttpGet hinzufügen. 
         */ 




        /// <summary>
        /// Lese alle Filme
        /// </summary>
        /// <returns>Eine List von Filmen</returns>
        [HttpGet("GetAllMovies")]
        public ActionResult<IEnumerable<Movie>> GetAllMovies()
        {
            return _context.Movie.ToList();
        }




        // GET: api/Movie

        /// <summary>
        /// GetMovie
        /// </summary>
        /// <returns>Eine Liste von Filme</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }
            return await _context.Movie.ToListAsync();
        }

        // GET: api/Movie/5

        /// <summary>
        /// Movie mit einer bestimmten Id
        /// </summary>
        /// <param name="id">Id - Wert</param>
        /// <returns>Film mit der gefragten Id</returns>
        [HttpGet("{id}")]
       
        //Wir können jeden Status-Code für die Dokumentation definieren. 
        //Variante 1:
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status304NotModified)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]


        //Variante 2:
        //Es gibt aber eine kleine Abkürzung:
        //[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        //AbiConventionMethod löst sich in mehrere ProducesResponseType auf

        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }
            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MovieDbContext.Movie'  is null.");
            }
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movie/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
