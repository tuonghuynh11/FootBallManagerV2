using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Repositories;

namespace FootBallManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballmatchesController : ControllerBase
    {
        private readonly IFootballmatchRepository _footballmatchRepo;

        public FootballmatchesController(IFootballmatchRepository footballmatchRepository)
        {
            _footballmatchRepo = footballmatchRepository;
        }

        // GET: api/Footballmatches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Footballmatch>>> GetFootballmatches()
        {
            try
            {
                return Ok(await _footballmatchRepo.GetAllFootballmatch());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Footballmatches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Footballmatch>> GetFootballmatch(int id)
        {
            try
            {

                var footballmatch = await _footballmatchRepo.GetFootballmatch(id);
                return footballmatch == null ? NotFound() : Ok(footballmatch);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Footballmatches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFootballmatch(int id, Footballmatch footballmatch)
        {
            try
            {

                if (id != footballmatch.Id)
                {
                    return NotFound();
                }
                await _footballmatchRepo.updateFootballmatchAsync(id, footballmatch);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Footballmatches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Footballmatch>> PostFootballmatch(Footballmatch footballmatch)
        {
            try
            {
                var newFootballmatch = await _footballmatchRepo.addFootballmatchAsync(footballmatch);
                return newFootballmatch == null ? NotFound() : Ok(newFootballmatch);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Footballmatches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFootballmatch(int id)
        {
            try
            {

                await _footballmatchRepo.deleteFootballmatchAsync(id);
                return Ok();
            }
            catch { return BadRequest(); }
        }

    }
}
