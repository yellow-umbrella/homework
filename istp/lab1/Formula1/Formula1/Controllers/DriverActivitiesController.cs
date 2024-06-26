﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Formula1;

namespace Formula1.Controllers
{
    public class DriverActivitiesController : Controller
    {
        private readonly DBFormula1Context _context;

        public DriverActivitiesController(DBFormula1Context context)
        {
            _context = context;
        }

        // GET: DriverActivities
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Teams");
            }
            ViewBag.TeamId = id;

            var dBFormula1Context = _context.DriverActivities.Where(d => d.TeamId == id).Include(d => d.Driver).Include(d => d.Season).Include(d => d.Team);
            return View(await dBFormula1Context.ToListAsync());
        }

        // GET: DriverActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DriverActivities == null)
            {
                return NotFound();
            }

            var driverActivity = await _context.DriverActivities
                .Include(d => d.Driver)
                .Include(d => d.Season)
                .Include(d => d.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driverActivity == null)
            {
                return NotFound();
            }

            return View(driverActivity);
        }

        // GET: DriverActivities/Create
        public IActionResult Create(int teamId)
        {
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Name");
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "Year");
            //ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            ViewBag.TeamId = teamId;
            return View();
        }

        // POST: DriverActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int teamId, [Bind("Id,TeamId,SeasonId,DriverId")] DriverActivity driverActivity)
        {
            driverActivity.TeamId = teamId;

            var activity = driverActivity;
            activity.Team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == driverActivity.TeamId);
            activity.Season = await _context.Seasons.FirstOrDefaultAsync(m => m.Id == driverActivity.SeasonId);
            activity.Driver = await _context.Drivers.FirstOrDefaultAsync(m => m.Id == driverActivity.DriverId);
            
            if (ModelState.IsValid && Validate(activity))
            {
                _context.Add(driverActivity);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "DriverActivities", new { id = teamId });
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Name", driverActivity.DriverId);
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "Year", driverActivity.SeasonId);
            //ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", driverActivity.TeamId);
            ViewBag.TeamId = teamId;
            
            return View(driverActivity);
        }

        private bool Validate(DriverActivity activity, int id = 0)
        {
            bool check1 = _context.DriverActivities.Any(d => d.TeamId == activity.TeamId &&
                                                    d.SeasonId == activity.SeasonId &&
                                                    d.DriverId == activity.DriverId &&
                                                    d.Id != id);
            bool check2 = activity.Driver.CareerStartYear > activity.Season.Year;
            bool check3 = activity.Team.FoundationYear > activity.Season.Year;

            if (check1)
            {
                ViewBag.error = "Помилка додавання! Цей гонщик уже доданий в цю команду цього сезону";
            }
            if (check2)
            {
                ViewBag.error = "Помилка додавання! Цей гонщик почав свою кар'єру пізніше цього сезону";
            }
            if (check3)
            {
                ViewBag.error = "Помилка додавання! Ця команда не існувала під час цього сезону";
            }
            return !(check1 || check2|| check3);
        }

        // GET: DriverActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DriverActivities == null)
            {
                return NotFound();
            }

            var driverActivity = await _context.DriverActivities.FindAsync(id);
            if (driverActivity == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Name", driverActivity.DriverId);
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "Year", driverActivity.SeasonId);
            ViewBag.TeamId = driverActivity.TeamId;
            //ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", driverActivity.TeamId);
            return View(driverActivity);
        }

        // POST: DriverActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamId,SeasonId,DriverId")] DriverActivity driverActivity)
        {
            if (id != driverActivity.Id)
            {
                return NotFound();
            }
            var activity = driverActivity;
            activity.Team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == driverActivity.TeamId);
            activity.Season = await _context.Seasons.FirstOrDefaultAsync(m => m.Id == driverActivity.SeasonId);
            activity.Driver = await _context.Drivers.FirstOrDefaultAsync(m => m.Id == driverActivity.DriverId);

            if (ModelState.IsValid && Validate(activity, id))
            {
                try
                {
                    _context.Update(driverActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverActivityExists(driverActivity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "DriverActivities", new { id = driverActivity.TeamId });
                //return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Name", driverActivity.DriverId);
            ViewData["SeasonId"] = new SelectList(_context.Seasons, "Id", "Year", driverActivity.SeasonId);
            //ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", driverActivity.TeamId);
            ViewBag.TeamId = driverActivity.TeamId;
            return View(driverActivity);
        }

        // GET: DriverActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DriverActivities == null)
            {
                return NotFound();
            }

            var driverActivity = await _context.DriverActivities
                .Include(d => d.Driver)
                .Include(d => d.Season)
                .Include(d => d.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driverActivity == null)
            {
                return NotFound();
            }
            ViewBag.TeamId = driverActivity.TeamId;

            return View(driverActivity);
        }

        // POST: DriverActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DriverActivities == null)
            {
                return Problem("Entity set 'DBFormula1Context.DriverActivities'  is null.");
            }
            var driverActivity = await _context.DriverActivities.FindAsync(id);
            if (driverActivity != null)
            {
                _context.DriverActivities.Remove(driverActivity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "DriverActivities", new { id = driverActivity.TeamId });
            //return RedirectToAction(nameof(Index));
        }

        private bool DriverActivityExists(int id)
        {
          return (_context.DriverActivities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
