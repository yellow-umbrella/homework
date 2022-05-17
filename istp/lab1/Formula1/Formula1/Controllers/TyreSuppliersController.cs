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
    public class TyreSuppliersController : Controller
    {
        private readonly DBFormula1Context _context;

        public TyreSuppliersController(DBFormula1Context context)
        {
            _context = context;
        }

        // GET: TyreSuppliers
        public async Task<IActionResult> Index()
        {
              return _context.TyreSuppliers != null ? 
                          View(await _context.TyreSuppliers.ToListAsync()) :
                          Problem("Entity set 'DBFormula1Context.TyreSuppliers'  is null.");
        }

        // GET: TyreSuppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TyreSuppliers == null)
            {
                return NotFound();
            }

            var tyreSupplier = await _context.TyreSuppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tyreSupplier == null)
            {
                return NotFound();
            }

            return View(tyreSupplier);
        }

        // GET: TyreSuppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TyreSuppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TyreSupplier tyreSupplier)
        {
            bool check = _context.TyreSuppliers.Any(c => c.Name == tyreSupplier.Name);

            if (ModelState.IsValid && !check)
            {
                _context.Add(tyreSupplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (check)
            {
                ViewBag.error = "Помилка додавання! Такий поставщик шин уже існує";
            }
            return View(tyreSupplier);
        }

        // GET: TyreSuppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TyreSuppliers == null)
            {
                return NotFound();
            }

            var tyreSupplier = await _context.TyreSuppliers.FindAsync(id);
            if (tyreSupplier == null)
            {
                return NotFound();
            }
            return View(tyreSupplier);
        }

        // POST: TyreSuppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TyreSupplier tyreSupplier)
        {
            if (id != tyreSupplier.Id)
            {
                return NotFound();
            }
            bool check = _context.TyreSuppliers.Any(c => c.Name == tyreSupplier.Name
                                                            && c.Id != id);

            if (ModelState.IsValid && !check)
            {
                try
                {
                    _context.Update(tyreSupplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TyreSupplierExists(tyreSupplier.Id))
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
                ViewBag.error = "Помилка додавання! Такий поставщик шин уже існує";
            }
            return View(tyreSupplier);
        }

        // GET: TyreSuppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TyreSuppliers == null)
            {
                return NotFound();
            }

            var tyreSupplier = await _context.TyreSuppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tyreSupplier == null)
            {
                return NotFound();
            }

            return View(tyreSupplier);
        }

        // POST: TyreSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TyreSuppliers == null)
            {
                return Problem("Entity set 'DBFormula1Context.TyreSuppliers'  is null.");
            }
            var tyreSupplier = await _context.TyreSuppliers.FindAsync(id);
            if (tyreSupplier != null)
            {
                _context.TyreSuppliers.Remove(tyreSupplier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TyreSupplierExists(int id)
        {
          return (_context.TyreSuppliers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
