using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeoDbApp.Data;
using GeoDbApp.Models;

namespace GeoDbApp.Controllers
{
    public class LanguagesInCountriesController : Controller
    {
        private readonly GeoDbContext _context;

        public LanguagesInCountriesController(GeoDbContext context)
        {
            _context = context;
        }

        // GET: LanguagesInCountries
        public async Task<IActionResult> Index()
            =>  View(await _context.LanguagesInCountries.ToListAsync());


        // GET: LanguagesInCountries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LanguagesInCountries == null)
            {
                return NotFound();
            }

            var languagesInCountry = await _context.LanguagesInCountries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (languagesInCountry == null)
            {
                return NotFound();
            }

            return View(languagesInCountry);
        }

        // GET: LanguagesInCountries/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            return View();
        }

        // POST: LanguagesInCountries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CountryId,LanguageId,Percent")] LanguagesInCountry languagesInCountry)
        {
            ModelState.Remove("Country");
            ModelState.Remove("Language");


            #region Keine Doppelten Länder / Sprachen zuordnungen

            LanguagesInCountry? entryAvailable =  await _context.LanguagesInCountries.FirstOrDefaultAsync(e => e.LanguageId == languagesInCountry.LanguageId && e.CountryId == languagesInCountry.CountryId);

            if (entryAvailable != null)
            {
                ModelState.AddModelError("LanguageId", "Land mit Sprache schon verfügbar");
                ModelState.AddModelError("CountryId", "Land mit Sprache schon verfügbar");
            }

            #endregion

            #region Prozentangaben der Sprachen in einem Land darf nicht 100 Prozent übersteigen 
            IList<LanguagesInCountry> allAvailableEntries = await _context.LanguagesInCountries.Where(e=>e.CountryId == languagesInCountry.CountryId).ToListAsync();

            int availablePercent = 100 - allAvailableEntries.Sum(e => e.Percent);

            if (languagesInCountry.Percent > availablePercent)
            {
                ModelState.AddModelError("Percent", $"Aktuell könn wir noch {availablePercent}% vergeben werden");
            }
            #endregion

            if (ModelState.IsValid)
            {
                _context.Add(languagesInCountry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", languagesInCountry.CountryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", languagesInCountry.LanguageId);
            return View(languagesInCountry);
        }

        // GET: LanguagesInCountries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LanguagesInCountries == null)
            {
                return NotFound();
            }

            var languagesInCountry = await _context.LanguagesInCountries.FindAsync(id);
            if (languagesInCountry == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", languagesInCountry.CountryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", languagesInCountry.LanguageId);
            return View(languagesInCountry);
        }

        // POST: LanguagesInCountries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CountryId,LanguageId,Percent")] LanguagesInCountry languagesInCountry)
        {
            if (id != languagesInCountry.Id)
            {
                return NotFound();
            }


            ModelState.Remove("Country");
            ModelState.Remove("Language");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(languagesInCountry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguagesInCountryExists(languagesInCountry.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", languagesInCountry.CountryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", languagesInCountry.LanguageId);
            return View(languagesInCountry);
        }

        // GET: LanguagesInCountries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LanguagesInCountries == null)
            {
                return NotFound();
            }

            var languagesInCountry = await _context.LanguagesInCountries
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (languagesInCountry == null)
            {
                return NotFound();
            }

            return View(languagesInCountry);
        }

        // POST: LanguagesInCountries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LanguagesInCountries == null)
            {
                return Problem("Entity set 'GeoDbContext.LanguagesInCountries'  is null.");
            }
            var languagesInCountry = await _context.LanguagesInCountries.FindAsync(id);
            if (languagesInCountry != null)
            {
                _context.LanguagesInCountries.Remove(languagesInCountry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanguagesInCountryExists(int id)
        {
          return (_context.LanguagesInCountries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
