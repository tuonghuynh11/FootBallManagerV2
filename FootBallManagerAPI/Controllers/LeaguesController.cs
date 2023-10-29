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
    public class LeaguesController : ControllerBase
    {
        private readonly ILeagueRepository _leagueRepo;

        public LeaguesController(ILeagueRepository leagueRepository)
        {
            _leagueRepo = leagueRepository;
        }

        // GET: api/Leagues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<League>>> GetLeagues()
        {
            try
            {
                return Ok(await _leagueRepo.GetAllLeagueListAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Leagues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<League>> GetLeague(int id)
        {
            var league = await _leagueRepo.GetLeagueAsync(id);
            return league == null ? NotFound() : Ok(league);
        }

        // PUT: api/Leagues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeague(int id, League league)
        {
            if (id != league.Id)
            {
                return NotFound();
            }
            await _leagueRepo.updateLeagueAsync(id, league);
            return Ok();
        }

        // POST: api/Leagues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<League>> PostLeague(League league)
        {
            try
            {
                var newLeagueId = await _leagueRepo.addLeagueAsync(league);
                var leagueNew = await _leagueRepo.GetLeagueAsync(newLeagueId);
                return leagueNew == null ? NotFound() : Ok(leagueNew);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Leagues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague(int id)
        {
            await _leagueRepo.deleteLeagueAsync(id);
            return Ok();
        }

    }
}
