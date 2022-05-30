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
    public class CompanionsController : ControllerBase
    {
        private readonly HeroAPIContext _context;

        public CompanionsController(HeroAPIContext context)
        {
            _context = context;
        }

        // GET: api/Companions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Companion>>> GetCompanions()
        {
          if (_context.Companions == null)
          {
              return NotFound();
          }
            return await _context.Companions.ToListAsync();
        }

        // GET: api/Companions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Companion>> GetCompanion(int id)
        {
          if (_context.Companions == null)
          {
              return NotFound();
          }
            var companion = await _context.Companions.FindAsync(id);

            if (companion == null)
            {
                return NotFound();
            }

            return companion;
        }

        // PUT: api/Companions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanion(int id, Companion companion)
        {
            if (id != companion.Id)
            {
                return BadRequest();
            }

            if (_context.Companions.Any(s => s.Name == companion.Name && s.Id != companion.Id))
            {
                return Problem("Entity with this Name has already existed");
            }

            _context.Entry(companion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanionExists(id))
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

        // POST: api/Companions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Companion>> PostCompanion(Companion companion)
        {
          if (_context.Companions == null)
          {
              return Problem("Entity set 'HeroAPIContext.Companions'  is null.");
          }
            if (_context.Companions.Any(s => s.Name == companion.Name))
            {
                return Problem("Entity with this Name has already existed");
            }

            _context.Companions.Add(companion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanion", new { id = companion.Id }, companion);
        }

        // DELETE: api/Companions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanion(int id)
        {
            if (_context.Companions == null)
            {
                return NotFound();
            }
            var companion = await _context.Companions.FindAsync(id);
            if (companion == null)
            {
                return NotFound();
            }

            _context.Companions.Remove(companion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanionExists(int id)
        {
            return (_context.Companions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
