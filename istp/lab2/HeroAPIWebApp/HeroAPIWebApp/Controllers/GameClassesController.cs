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
    public class GameClassesController : ControllerBase
    {
        private readonly HeroAPIContext _context;

        public GameClassesController(HeroAPIContext context)
        {
            _context = context;
        }

        // GET: api/GameClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameClass>>> GetGameClasses()
        {
          if (_context.GameClasses == null)
          {
              return NotFound();
          }
            return await _context.GameClasses.ToListAsync();
        }

        // GET: api/GameClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameClass>> GetGameClass(int id)
        {
          if (_context.GameClasses == null)
          {
              return NotFound();
          }
            var gameClass = await _context.GameClasses.FindAsync(id);

            if (gameClass == null)
            {
                return NotFound();
            }

            return gameClass;
        }

        // PUT: api/GameClasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameClass(int id, GameClass gameClass)
        {
            if (id != gameClass.Id)
            {
                return BadRequest();
            }

            if (_context.GameClasses.Any(s => s.Name == gameClass.Name && s.Id != gameClass.Id))
            {
                return Problem("Entity with this Name has already existed");
            }

            _context.Entry(gameClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameClassExists(id))
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

        // POST: api/GameClasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameClass>> PostGameClass(GameClass gameClass)
        {
          if (_context.GameClasses == null)
          {
              return Problem("Entity set 'HeroAPIContext.GameClasses'  is null.");
          }
            if (_context.GameClasses.Any(s => s.Name == gameClass.Name))
            {
                return Problem("Entity with this Name has already existed");
            }
            _context.GameClasses.Add(gameClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameClass", new { id = gameClass.Id }, gameClass);
        }

        // DELETE: api/GameClasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameClass(int id)
        {
            if (_context.GameClasses == null)
            {
                return NotFound();
            }
            var gameClass = await _context.GameClasses.FindAsync(id);
            if (gameClass == null)
            {
                return NotFound();
            }

            _context.GameClasses.Remove(gameClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameClassExists(int id)
        {
            return (_context.GameClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
