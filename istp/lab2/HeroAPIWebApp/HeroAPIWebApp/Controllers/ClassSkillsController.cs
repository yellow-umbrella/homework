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
    public class ClassSkillsController : ControllerBase
    {
        private readonly HeroAPIContext _context;

        public ClassSkillsController(HeroAPIContext context)
        {
            _context = context;
        }

        // GET: api/ClassSkills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassSkill>>> GetClassSkills()
        {
          if (_context.ClassSkills == null)
          {
              return NotFound();
          }
            return await _context.ClassSkills.ToListAsync();
        }

        // GET: api/ClassSkills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassSkill>> GetClassSkill(int id)
        {
            if (_context.ClassSkills == null)
            {
                return NotFound();
            }
          
            var classSkill = await _context.ClassSkills.FindAsync(id);

            if (classSkill == null)
            {
                return NotFound();
            }

            return classSkill;
        }

        // PUT: api/ClassSkills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassSkill(int id, ClassSkill classSkill)
        {
            if (id != classSkill.Id)
            {
                return BadRequest();
            }

            if (_context.ClassSkills.Any(s => s.ClassId == classSkill.ClassId && s.SkillId == classSkill.SkillId
                                         && s.Id != classSkill.Id))
            {
                return Problem("Entity with this parameters has already existed");
            }

            _context.Entry(classSkill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassSkillExists(id))
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

        // POST: api/ClassSkills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassSkill>> PostClassSkill(ClassSkill classSkill)
        {
            if (_context.ClassSkills == null)
            {
                return Problem("Entity set 'HeroAPIContext.ClassSkills'  is null.");
            }

            if (_context.ClassSkills.Any(s => s.ClassId == classSkill.ClassId && s.SkillId == classSkill.SkillId))
            {
                return Problem("Entity with this parameters has already existed");
            }

            _context.ClassSkills.Add(classSkill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassSkill", new { id = classSkill.Id }, classSkill);
        }

        // DELETE: api/ClassSkills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassSkill(int id)
        {
            if (_context.ClassSkills == null)
            {
                return NotFound();
            }
            var classSkill = await _context.ClassSkills.FindAsync(id);
            if (classSkill == null)
            {
                return NotFound();
            }

            _context.ClassSkills.Remove(classSkill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassSkillExists(int id)
        {
            return (_context.ClassSkills?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
