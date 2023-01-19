using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_with_EFCore_Relationen_EagerLoading.Data;
using WebAPI_with_EFCore_Relationen_EagerLoading.Models;

namespace WebAPI_with_EFCore_Relationen_EagerLoading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContinentController : ControllerBase
    {
        private readonly GeoDbContext _context;

        public ContinentController(GeoDbContext context)
        {
            _context = context;
        }

        // GET: api/Continent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Continent>>> GetContinents(int pageNumber = 1, int itemsToShow = 3)
        {
            if (_context.Continents == null)
            {
                return NotFound();
            }

            IList<Continent> continents = await _context.Continents.Include("Countries").ToListAsync();

            return Ok(continents.Skip((pageNumber - 1) * itemsToShow).Take(itemsToShow));
        }

        // GET: api/Continent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Continent>> GetContinent(int id)
        {
          if (_context.Continents == null)
          {
              return NotFound();
          }
            var continent = await _context.Continents.FindAsync(id);

            if (continent == null)
            {
                return NotFound();
            }

            return continent;
        }

        // PUT: api/Continent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContinent(int id, Continent continent)
        {
            if (id != continent.Id)
            {
                return BadRequest();
            }

            _context.Entry(continent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContinentExists(id))
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

        // POST: api/Continent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Continent>> PostContinent(Continent continent)
        {
          if (_context.Continents == null)
          {
              return Problem("Entity set 'GeoDbContext.Continents'  is null.");
          }
            _context.Continents.Add(continent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContinent", new { id = continent.Id }, continent);
        }

        // DELETE: api/Continent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContinent(int id)
        {
            if (_context.Continents == null)
            {
                return NotFound();
            }
            var continent = await _context.Continents.FindAsync(id);
            if (continent == null)
            {
                return NotFound();
            }

            _context.Continents.Remove(continent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContinentExists(int id)
        {
            return (_context.Continents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
