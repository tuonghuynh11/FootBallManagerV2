using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamOfLeaguesController : ControllerBase
    {
        private readonly ITeamOfLeagueRepository _teamOfLeagueRepo;

        public TeamOfLeaguesController(ITeamOfLeagueRepository repo)
        {
            _teamOfLeagueRepo = repo;
        }

        // GET: api/TeamOfLeagues
        [HttpGet]
        public async Task<IActionResult> GetTeamofleagues()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _teamOfLeagueRepo.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/TeamOfLeagues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Teamofleague>>> GetTeamofleague(int id)
        {
            try
            {
                var teamOfLeague = await _teamOfLeagueRepo.GetById(id);

                if (teamOfLeague == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = teamOfLeague,

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/TeamOfLeagues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTeamofleague(int id, Teamofleague teamofleague)
        {
            try
            {
                if (id != teamofleague.Id)
                {
                    return BadRequest();
                }

                try
                {
                    await _teamOfLeagueRepo.Update(teamofleague);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamofleagueExists(id))
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
            catch
            {

                return Problem();
            }
        }


        //PATCH
        [HttpPatch("patch/{id}")]
        [Authorize]
        public async Task<IActionResult> PatchThongtingiaidau(int id, JsonPatchDocument teamOfLeagueModel)
        {
            try
            {
                var isSuccess = await _teamOfLeagueRepo.Patch(id, teamOfLeagueModel);

                if (isSuccess)
                {
                    return NoContent();

                }
                else
                {
                    return BadRequest(new { message = "Id not exists" });
                }

            }
            catch
            {

                return Problem();
            }

        }



        // POST: api/TeamOfLeagues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]
        public async Task<ActionResult<Teamofleague>> PostTeamofleague(Teamofleague teamofleague)
        {
            try
            {
                int newId = await _teamOfLeagueRepo.Create(teamofleague);

                return CreatedAtAction("GetTeamofleague", new { id = newId }, teamofleague);
            }
            catch
            {

                return Problem();
            }
        }

        // DELETE: api/TeamOfLeagues/5
        [HttpDelete("delete/{idGiaiDau}/{idTeam}")]
        [Authorize]
        public async Task<IActionResult> DeleteTeamofleague(int idGiaiDau,string idTeam)
        {
            try
            {
                var isSuccess = await _teamOfLeagueRepo.Delete(idGiaiDau,idTeam);
                if (!isSuccess)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch
            {

                return Problem();
            }
        }


        //Check team is join the league
        [HttpPost("join/{idLeague}/{idTeam}")]
        public async Task<IActionResult> IsTeamJoinLeague(int idLeague, string idTeam)
        {
            try
            {
                bool isExist =await _teamOfLeagueRepo.IsTeamJoinLeague(idLeague,idTeam);
                
                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    isExist = isExist,
                });
            }
            catch 
            {

                return BadRequest();
            }
        }


        private bool TeamofleagueExists(int id)
        {
            return  _teamOfLeagueRepo.TeamofleagueExists(id);
        }
    }
}
