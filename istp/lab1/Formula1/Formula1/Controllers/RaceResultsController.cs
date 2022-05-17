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
    public class RaceResultsController : Controller
    {
        private readonly DBFormula1Context _context;

        public RaceResultsController(DBFormula1Context context)
        {
            _context = context;
        }

        // GET: RaceResults
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Races");
            }
            ViewBag.RaceId = id;
            var race = await _context.Races.FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.SeasonId = race.SeasonId;
            var dBFormula1Context = _context.RaceResults.Where(r => r.RaceId == id).Include(r => r.Driver).Include(r => r.Race);
            return View(await dBFormula1Context.ToListAsync());
        }

        // GET: RaceResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RaceResults == null)
            {
                return NotFound();
            }

            var raceResult = await _context.RaceResults
                .Include(r => r.Driver)
                .Include(r => r.Race)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raceResult == null)
            {
                return NotFound();
            }

            return View(raceResult);
        }

        // GET: RaceResults/Create
        public IActionResult Create(int raceId)
        {
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Name");
            //ViewData["RaceId"] = new SelectList(_context.Races, "Id", "Name");
            ViewBag.RaceId = raceId;
            return View();
        }

        // POST: RaceResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int raceId, [Bind("Id,RaceId,DriverId,Place,Points")] RaceResult raceResult)
        {
            raceResult.RaceId = raceId;
            if (ModelState.IsValid && Validate(raceResult))
            {
                _context.Add(raceResult);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "RaceResults", new { id = raceId });
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Name", raceResult.DriverId);
            //ViewData["RaceId"] = new SelectList(_context.Races, "Id", "Name", raceResult.RaceId);
            ViewBag.RaceId = raceId;
            return View(raceResult);
        }

        bool Validate(RaceResult raceResult, int id = 0)
        {
            bool check1 = _context.RaceResults.Any(d => d.DriverId == raceResult.DriverId 
                                                    && d.RaceId == raceResult.RaceId
                                                    && d.Id != id);
            bool check2 = _context.RaceResults.Any(d => d.Place == raceResult.Place
                                                    && d.RaceId == raceResult.RaceId
                                                    && d.Id != id);

            if (check1)
            {
                ViewBag.error = "Помилка додавання! Результати гонщика уже були додані";
            }
            if (check2)
            {
                ViewBag.error = "Помилка додавання! Це місце уже зайняте";
            }
            
            return !(check1 || check2);
        }

        // GET: RaceResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RaceResults == null)
            {
                return NotFound();
            }

            var raceResult = await _context.RaceResults.FindAsync(id);
            if (raceResult == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Name", raceResult.DriverId);
            //ViewData["RaceId"] = new SelectList(_context.Races, "Id", "Name", raceResult.RaceId);
            ViewBag.RaceId = raceResult.RaceId;
            return View(raceResult);
        }

        // POST: RaceResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RaceId,DriverId,Place,Points")] RaceResult raceResult)
        {
            if (id != raceResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && Validate(raceResult, id))
            {
                try
                {
                    _context.Update(raceResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceResultExists(raceResult.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "RaceResults", new { id = raceResult.RaceId });
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Name", raceResult.DriverId);
            //ViewData["RaceId"] = new SelectList(_context.Races, "Id", "Name", raceResult.RaceId);
            ViewBag.RaceId = raceResult.RaceId;
            return View(raceResult);
        }

        // GET: RaceResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RaceResults == null)
            {
                return NotFound();
            }

            var raceResult = await _context.RaceResults
                .Include(r => r.Driver)
                .Include(r => r.Race)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raceResult == null)
            {
                return NotFound();
            }
            ViewBag.RaceId = raceResult.RaceId;

            return View(raceResult);
        }

        // POST: RaceResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RaceResults == null)
            {
                return Problem("Entity set 'DBFormula1Context.RaceResults'  is null.");
            }
            var raceResult = await _context.RaceResults.FindAsync(id);
            if (raceResult != null)
            {
                _context.RaceResults.Remove(raceResult);
            }
            
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "RaceResults", new { id = raceResult.RaceId });

        }

        private bool RaceResultExists(int id)
        {
          return (_context.RaceResults?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
