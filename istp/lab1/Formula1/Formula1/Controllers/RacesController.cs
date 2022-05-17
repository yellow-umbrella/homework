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
    public class RacesController : Controller
    {
        private readonly DBFormula1Context _context;

        public RacesController(DBFormula1Context context)
        {
            _context = context;
        }

        // GET: Races
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Seasons");
            }
            ViewBag.SeasonId = id;

            var dBFormula1Context = _context.Races.Where(r => r.SeasonId == id).Include(r => r.Circuite).Include(r => r.Season);
            return View(await dBFormula1Context.ToListAsync());
        }

        // GET: Races/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Races == null)
            {
                return NotFound();
            }

            var race = await _context.Races
                .Include(r => r.Circuite)
                .Include(r => r.Season)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (race == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "RaceResults", new { id = race.Id });
            //return View(race);
        }

        // GET: Races/Create
        public IActionResult Create(int seasonId)
        {
            ViewData["CircuiteId"] = new SelectList(_context.Circuites, "Id", "Name");
            //ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "Name");
            ViewBag.SeasonId = seasonId;
            return View();
        }

        // POST: Races/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int seasonId, [Bind("Id,SeasonId,CircuiteId,Date,LapCount")] Race race)
        {
            race.SeasonId = seasonId;
            var _race = race;
            _race.Season = await _context.Seasons.FirstOrDefaultAsync(m => m.Id == race.SeasonId);
            if (ModelState.IsValid && Validate(_race))
            {
                _context.Add(race);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Races", new { id = seasonId });
            }
            ViewData["CircuiteId"] = new SelectList(_context.Circuites, "Id", "Name", race.CircuiteId);
            ViewBag.SeasonId = seasonId;
            //ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "Name", race.SeasonId);
            return View(race);
        }

        bool Validate(Race race, int id = 0)
        {
            bool check1 = _context.Races.Any(d => d.Date == race.Date
                                                    && d.Id != id);
            bool check2 = race.Date.Year != race.Season.Year;

            if (check1)
            {
                ViewBag.error = "Помилка додавання! Така гонка уже існує";
            }
            if (check2)
            {
                ViewBag.error = "Помилка додавання! Вказано неправильний рік";
            }

            return !(check1 || check2);
        }

        // GET: Races/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Races == null)
            {
                return NotFound();
            }

            var race = await _context.Races.FindAsync(id);
            if (race == null)
            {
                return NotFound();
            }
            ViewData["CircuiteId"] = new SelectList(_context.Circuites, "Id", "Name", race.CircuiteId);
            //ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "Name", race.SeasonId);
            ViewBag.SeasonId = race.SeasonId;

            return View(race);
        }

        // POST: Races/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SeasonId,CircuiteId,Date,LapCount")] Race race)
        {
            if (id != race.Id)
            {
                return NotFound();
            }
            var _race = race;
            _race.Season = await _context.Seasons.FirstOrDefaultAsync(m => m.Id == race.SeasonId);
            if (ModelState.IsValid && Validate(_race, id))
            {
                try
                {
                    _context.Update(race);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceExists(race.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Races", new { id = race.SeasonId });
                //return RedirectToAction(nameof(Index));
            }
            ViewData["CircuiteId"] = new SelectList(_context.Circuites, "Id", "Name", race.CircuiteId);
            //ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "Name", race.SeasonId);
            ViewBag.SeasonId = race.SeasonId;
            return View(race);
        }

        // GET: Races/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Races == null)
            {
                return NotFound();
            }

            var race = await _context.Races
                .Include(r => r.Circuite)
                .Include(r => r.Season)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (race == null)
            {
                return NotFound();
            }
            ViewBag.SeasonId = race.SeasonId;
            return View(race);
        }

        // POST: Races/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Races == null)
            {
                return Problem("Entity set 'DBFormula1Context.Races'  is null.");
            }
            var race = await _context.Races.FindAsync(id);
            if (race != null)
            {
                _context.Races.Remove(race);
            }
            
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Races", new { id = race.SeasonId });
        }

        private bool RaceExists(int id)
        {
          return (_context.Races?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
