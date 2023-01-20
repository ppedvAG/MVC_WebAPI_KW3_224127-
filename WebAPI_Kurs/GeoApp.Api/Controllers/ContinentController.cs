﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoApp.Api.Data;
using GeoApp.Shared.Entities;

namespace GeoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContinentController : ControllerBase
    {
        private readonly GeoAppContext _context;

        public ContinentController(GeoAppContext context)
        {
            _context = context;
        }

        // GET: api/Continent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Continent>>> GetContinent()
        {
          if (_context.Continent == null)
          {
              return NotFound();
          }
            return await _context.Continent.ToListAsync();
        }

        // GET: api/Continent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Continent>> GetContinent(int id)
        {
          if (_context.Continent == null)
          {
              return NotFound();
          }
            var continent = await _context.Continent.FindAsync(id);

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
          if (_context.Continent == null)
          {
              return Problem("Entity set 'GeoAppContext.Continent'  is null.");
          }
            _context.Continent.Add(continent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContinent", new { id = continent.Id }, continent);
        }

        // DELETE: api/Continent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContinent(int id)
        {
            if (_context.Continent == null)
            {
                return NotFound();
            }
            var continent = await _context.Continent.FindAsync(id);
            if (continent == null)
            {
                return NotFound();
            }

            _context.Continent.Remove(continent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContinentExists(int id)
        {
            return (_context.Continent?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
