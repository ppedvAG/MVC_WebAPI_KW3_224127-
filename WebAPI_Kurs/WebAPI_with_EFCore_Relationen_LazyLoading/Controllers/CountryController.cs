using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Grundlagen.DTO.Mapper;
using WebAPI_with_EFCore_Relationen_LazyLoading.Data;
using WebAPI_with_EFCore_Relationen_LazyLoading.DTO;
using WebAPI_with_EFCore_Relationen_LazyLoading.Models;

namespace WebAPI_with_EFCore_Relationen_LazyLoading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly GeoDbContext _context;

        public CountryController(GeoDbContext context)
        {
            _context = context;
        }

        // GET: api/Country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountries()
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            IList<Country> countries = await _context.Countries.ToListAsync();

            return Ok(countries.ToDTOs());
        }

        // GET: api/Country/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id)
        {
          if (_context.Countries == null)
          {
              return NotFound();
          }
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country.ToDTO();
        }

        // PUT: api/Country/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, CountryDTO countryDTO)
        {

            Country country = countryDTO.ToEntity();

            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Country
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CountryDTO countryDTO)
        {
          if (_context.Countries == null)
          {
              return Problem("Entity set 'GeoDbContext.Countries'  is null.");
          }

            Country country = countryDTO.ToEntity();

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Country/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
