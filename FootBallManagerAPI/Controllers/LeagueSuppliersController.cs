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
    public class LeagueSuppliersController : ControllerBase
    {
        private readonly ILeagueSupplierRepository _leagueSupplierRepos;

        public LeagueSuppliersController(ILeagueSupplierRepository repo)
        {
            _leagueSupplierRepos = repo;
        }


        // GET: api/LeagueSuppliers
        [HttpGet]
        public async Task<IActionResult> GetLeaguesuppliers()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _leagueSupplierRepos.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/LeagueSuppliers/5
        [HttpGet("{idSupplier}/{idLeague}")]
        public async Task<ActionResult<Leaguesupplier>> GetLeaguesupplier(int idSupplier, int idLeague)
        {
            try
            {
                var leagueSupplier = await _leagueSupplierRepos.GetById(idSupplier, idLeague);

                if (leagueSupplier == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = leagueSupplier,

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/LeagueSuppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{idSupplier}/{idLeague}")]
        [Authorize]
        public async Task<IActionResult> UpdateLeaguesupplier(int idSupplier, int idLeague, Leaguesupplier leaguesupplier)
        {
            try
            {
                if ((idSupplier != leaguesupplier.IdSupplier) || (idLeague != leaguesupplier.IdLeague))
                {
                    return BadRequest();
                }

                try
                {
                    await _leagueSupplierRepos.Update(leaguesupplier);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaguesupplierExists(idSupplier, idLeague))
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
        [HttpPatch("patch/{idSupplier}/{idLeague}")]
        [Authorize]
        public async Task<IActionResult> PatchThongtingiaidau(int idSupplier, int idLeague, JsonPatchDocument leagueSupplierModel)
        {
            try
            {
                var isSuccess = await _leagueSupplierRepos.Patch(idSupplier, idLeague, leagueSupplierModel);

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

        // POST: api/LeagueSuppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]
        public async Task<ActionResult<Leaguesupplier>> PostLeaguesupplier(Leaguesupplier leaguesupplier)
        {
            try
            {
                int newId = await _leagueSupplierRepos.Create(leaguesupplier);

                return CreatedAtAction("GetLeaguesupplier", new { idSupplier = leaguesupplier.IdSupplier, idLeague = leaguesupplier.IdLeague }, leaguesupplier);
            }
            catch
            {

                return Problem();
            }
        }

        // DELETE: api/LeagueSuppliers/5
        [HttpDelete("delete/{idSupplier}/{idLeague}")]
        [Authorize]
        public async Task<IActionResult> DeleteLeaguesupplier(int idSupplier, int idLeague)
        {
            try
            {
                var isSuccess = await _leagueSupplierRepos.Delete(idSupplier, idLeague);
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

        private bool LeaguesupplierExists(int idSupplier, int idLeague)
        {
            return _leagueSupplierRepos.GetById(idSupplier, idLeague) != null;
        }
    }
}
