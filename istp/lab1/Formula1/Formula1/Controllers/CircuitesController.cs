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
    public class CircuitesController : Controller
    {
        private readonly DBFormula1Context _context;

        public CircuitesController(DBFormula1Context context)
        {
            _context = context;
        }

        // GET: Circuites
        public async Task<IActionResult> Index()
        {
            var dBFormula1Context = _context.Circuites.Include(c => c.Country);
            return View(await dBFormula1Context.ToListAsync());
        }

        // GET: Circuites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Circuites == null)
            {
                return NotFound();
            }

            var circuite = await _context.Circuites
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circuite == null)
            {
                return NotFound();
            }

            return View(circuite);
        }

        // GET: Circuites/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: Circuites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CountryId,Name")] Circuite circuite)
        {
            bool check = _context.Circuites.Any(c => c.CountryId == circuite.CountryId && 
                                                    c.Name == circuite.Name);
            if (ModelState.IsValid && !check)
            {
                _context.Add(circuite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (check)
            {
                ViewBag.error = "Помилка додавання! Така траса уже існує в цій країні";
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", circuite.CountryId);
            return View(circuite);
        }

        // GET: Circuites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Circuites == null)
            {
                return NotFound();
            }

            var circuite = await _context.Circuites.FindAsync(id);
            if (circuite == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", circuite.CountryId);
            return View(circuite);
        }

        // POST: Circuites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CountryId,Name")] Circuite circuite)
        {
            if (id != circuite.Id)
            {
                return NotFound();
            }

            bool check = _context.Circuites.Any(c => c.CountryId == circuite.CountryId &&
                                                    c.Name == circuite.Name && c.Id != circuite.Id);

            if (ModelState.IsValid && !check)
            {
                try
                {
                    _context.Update(circuite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CircuiteExists(circuite.Id))
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
            if (check)
            {
                ViewBag.error = "Помилка додавання! Така траса уже існує в цій країні";
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", circuite.CountryId);
            return View(circuite);
        }

        // GET: Circuites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Circuites == null)
            {
                return NotFound();
            }

            var circuite = await _context.Circuites
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circuite == null)
            {
                return NotFound();
            }

            return View(circuite);
        }

        // POST: Circuites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Circuites == null)
            {
                return Problem("Entity set 'DBFormula1Context.Circuites'  is null.");
            }
            var circuite = await _context.Circuites.FindAsync(id);
            if (circuite != null)
            {
                _context.Circuites.Remove(circuite);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CircuiteExists(int id)
        {
          return (_context.Circuites?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
