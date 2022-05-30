using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeroAPIWebApp.Models;

namespace HeroAPIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceSkillsController : ControllerBase
    {
        private readonly HeroAPIContext _context;

        public RaceSkillsController(HeroAPIContext context)
        {
            _context = context;
        }

        // GET: api/RaceSkills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceSkill>>> GetRaceSkills()
        {
          if (_context.RaceSkills == null)
          {
              return NotFound();
          }
            return await _context.RaceSkills.ToListAsync();
        }

        // GET: api/RaceSkills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RaceSkill>> GetRaceSkill(int id)
        {
          if (_context.RaceSkills == null)
          {
              return NotFound();
          }
            var raceSkill = await _context.RaceSkills.FindAsync(id);

            if (raceSkill == null)
            {
                return NotFound();
            }

            return raceSkill;
        }

        // PUT: api/RaceSkills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRaceSkill(int id, RaceSkill raceSkill)
        {
            if (id != raceSkill.Id)
            {
                return BadRequest();
            }

            if (_context.RaceSkills.Any(s => s.RaceId == raceSkill.RaceId && s.SkillId == raceSkill.SkillId
                                         && s.Id != raceSkill.Id))
            {
                return Problem("Entity with this parameters has already existed");
            }

            _context.Entry(raceSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceSkillExists(id))
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

        // POST: api/RaceSkills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RaceSkill>> PostRaceSkill(RaceSkill raceSkill)
        {
          if (_context.RaceSkills == null)
          {
              return Problem("Entity set 'HeroAPIContext.RaceSkills'  is null.");
          }
            if (_context.RaceSkills.Any(s => s.RaceId == raceSkill.RaceId && s.SkillId == raceSkill.SkillId))
            {
                return Problem("Entity with this parameters has already existed");
            }
            _context.RaceSkills.Add(raceSkill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRaceSkill", new { id = raceSkill.Id }, raceSkill);
        }

        // DELETE: api/RaceSkills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRaceSkill(int id)
        {
            if (_context.RaceSkills == null)
            {
                return NotFound();
            }
            var raceSkill = await _context.RaceSkills.FindAsync(id);
            if (raceSkill == null)
            {
                return NotFound();
            }

            _context.RaceSkills.Remove(raceSkill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RaceSkillExists(int id)
        {
            return (_context.RaceSkills?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
