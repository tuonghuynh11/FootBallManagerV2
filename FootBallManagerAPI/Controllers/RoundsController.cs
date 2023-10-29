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
    public class RoundsController : ControllerBase
    {
        private readonly IRoundRepository _roundRepo;

        public RoundsController(IRoundRepository repo)
        {
            _roundRepo = repo;
        }

        // GET: api/Rounds
        [HttpGet]
        public async Task<IActionResult> GetRounds()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _roundRepo.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/Rounds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Round>> GetRound(int id)
        {
            try
            {
                var round = await _roundRepo.GetById(id);

                if (round == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = round,

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/Rounds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> PutRound(int id, Round round)
        {
            try
            {
                if (id != round.Id)
                {
                    return BadRequest();
                }

                try
                {
                    await _roundRepo.Update(round);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoundExists(id))
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
        public async Task<IActionResult> PatchThongtingiaidau(int id, JsonPatchDocument roundModel)
        {
            try
            {
                var isSuccess = await _roundRepo.Patch(id, roundModel);

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
        // POST: api/Rounds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]

        public async Task<ActionResult<Round>> PostRound(Round round)
        {
            try
            {
                int newId = await _roundRepo.Create(round);

                return CreatedAtAction("GetRound", new { id = newId }, round);
            }
            catch
            {

                return Problem();
            }
        }

        // DELETE: api/Rounds/5
        [HttpDelete("delete/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteRound(int id)
        {
            try
            {
                var isSuccess = await _roundRepo.Delete(id);
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

        private bool RoundExists(int id)
        {
            return _roundRepo.GetById(id)!=null;
        }
    }
}
