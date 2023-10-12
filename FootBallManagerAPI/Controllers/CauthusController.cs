using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauthusController : ControllerBase
    {
        private readonly FootBallManagerV2Context _context;

        public CauthusController(FootBallManagerV2Context context)
        {
            _context = context;
        }

        // GET: api/Cauthus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cauthu>>> GetCauthus()
        {
          if (_context.Cauthus == null)
          {
              return NotFound();
          }
            return await _context.Cauthus.ToListAsync();
        }

        // GET: api/Cauthus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cauthu>> GetCauthu(int id)
        {
          if (_context.Cauthus == null)
          {
              return NotFound();
          }
            var cauthu = await _context.Cauthus.FindAsync(id);

            if (cauthu == null)
            {
                return NotFound();
            }

            return cauthu;
        }

        // PUT: api/Cauthus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCauthu(int id, Cauthu cauthu)
        {
            if (id != cauthu.Id)
            {
                return BadRequest();
            }

            _context.Entry(cauthu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CauthuExists(id))
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

        // POST: api/Cauthus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cauthu>> PostCauthu(Cauthu cauthu)
        {
          if (_context.Cauthus == null)
          {
              return Problem("Entity set 'FootBallManagerV2Context.Cauthus'  is null.");
          }
            _context.Cauthus.Add(cauthu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCauthu", new { id = cauthu.Id }, cauthu);
        }

        // DELETE: api/Cauthus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCauthu(int id)
        {
            if (_context.Cauthus == null)
            {
                return NotFound();
            }
            var cauthu = await _context.Cauthus.FindAsync(id);
            if (cauthu == null)
            {
                return NotFound();
            }

            _context.Cauthus.Remove(cauthu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CauthuExists(int id)
        {
            return (_context.Cauthus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
