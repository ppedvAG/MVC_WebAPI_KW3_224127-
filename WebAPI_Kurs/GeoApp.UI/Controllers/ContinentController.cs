using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeoApp.Shared.Entities;
using GeoApp.UI.Services;

namespace GeoApp.UI.Controllers
{
    public class ContinentController : Controller
    {
        private readonly IContinentService _service;

        public ContinentController(IContinentService service)
        {
            _service = service;
        }

        // GET: Continent
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllContinents());
        }

        // GET: Continent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = await _service.GetById(id.Value);

            if (continent == null)
            {
                return NotFound();
            }

            return View(continent);
        }

        // GET: Continent/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Continent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Continent continent)
        {
            ModelState.Remove("Countries");
            if (ModelState.IsValid)
            {
                await _service.AddContinent(continent);
                return RedirectToAction(nameof(Index));
            }
            return View(continent);
        }

        // GET: Continent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = await _service.GetById(id.Value);
            if (continent == null)
            {
                return NotFound();
            }
            return View(continent);
        }

        // POST: Continent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Continent continent)
        {
            if (id != continent.Id)
            {
                return NotFound();
            }

            continent.Countries = new List<Country>();

            ModelState.Remove("Countries");

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateContinent(continent);
                }
                catch (Exception ex)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(continent);
        }

        // GET: Continent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = await _service.GetById(id.Value);
            if (continent == null)
            {
                return NotFound();
            }

            return View(continent);
        }

        // POST: Continent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var continent = await _service.GetById(id);
            if (continent != null)
            {
                await _service.DeleteContinent(continent.Id);
            }

            return RedirectToAction(nameof(Index));
        }

       
    }
}
