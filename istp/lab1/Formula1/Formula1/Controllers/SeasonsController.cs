using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Formula1;

namespace Formula1.Controllers
{
    public class SeasonsController : Controller
    {
        private readonly DBFormula1Context _context;

        public SeasonsController(DBFormula1Context context)
        {
            _context = context;
        }

        // GET: Seasons
        public async Task<IActionResult> Index()
        {
            var dBFormula1Context = _context.Seasons.Include(s => s.TyreSupplier);
            return View(await dBFormula1Context.ToListAsync());
        }

        // GET: Seasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Seasons == null)
            {
                return NotFound();
            }

            var season = await _context.Seasons
                .Include(s => s.TyreSupplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (season == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Races", new {id = season.Id});
            //return View(season);
        }

        // GET: Seasons/Create
        public IActionResult Create()
        {

            ViewData["TyreSupplierId"] = new SelectList(_context.TyreSuppliers, "Id", "Name");
            return View();
        }

        // POST: Seasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TyreSupplierId,Year,RaceDirector,Rules")] Season season)
        {
            bool check = _context.Seasons.Any(s => s.Year == season.Year);
            if (ModelState.IsValid && !check)
            {
                _context.Add(season);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TyreSupplierId"] = new SelectList(_context.TyreSuppliers, "Id", "Name", season.TyreSupplierId);
            return View(season);
        }

        // GET: Seasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Seasons == null)
            {
                return NotFound();
            }

            var season = await _context.Seasons.FindAsync(id);
            if (season == null)
            {
                return NotFound();
            }
            ViewData["TyreSupplierId"] = new SelectList(_context.TyreSuppliers, "Id", "Name", season.TyreSupplierId);
            return View(season);
        }

        // POST: Seasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TyreSupplierId,Year,RaceDirector,Rules")] Season season)
        {
            if (id != season.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(season);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeasonExists(season.Id))
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
            ViewData["TyreSupplierId"] = new SelectList(_context.TyreSuppliers, "Id", "Name", season.TyreSupplierId);
            return View(season);
        }

        // GET: Seasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Seasons == null)
            {
                return NotFound();
            }

            var season = await _context.Seasons
                .Include(s => s.TyreSupplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
        }

        // POST: Seasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Seasons == null)
            {
                return Problem("Entity set 'DBFormula1Context.Seasons'  is null.");
            }
            var season = await _context.Seasons.FindAsync(id);
            if (season != null)
            {
                _context.Seasons.Remove(season);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeasonExists(int id)
        {
          return (_context.Seasons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
