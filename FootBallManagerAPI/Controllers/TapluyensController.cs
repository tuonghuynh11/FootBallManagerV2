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
    public class TapluyensController : ControllerBase
    {
        private readonly ITapLuyenRepository _tapLuyenRepos;

        public TapluyensController(ITapLuyenRepository repo)
        {
            _tapLuyenRepos = repo;
        }

        // GET: api/Tapluyens
        [HttpGet]
        public async Task<IActionResult> GetTapluyens()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _tapLuyenRepos.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/Tapluyens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tapluyen>> GetTapluyen(int id)
        {
            try
            {
                var tapluyen = await _tapLuyenRepos.GetById(id);

                if (tapluyen == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = tapluyen,

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/Tapluyens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTapluyen(int id, Tapluyen tapluyen)
        {
            try
            {
                if (id != tapluyen.Id)
                {
                    return BadRequest();
                }

                try
                {
                    await _tapLuyenRepos.Update(tapluyen);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TapluyenExists(id))
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
        public async Task<IActionResult> PatchTapLuyen(int id, JsonPatchDocument tapLuyenModel)
        {
            try
            {
                var isSuccess = await _tapLuyenRepos.Patch(id, tapLuyenModel);

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

        // POST: api/Tapluyens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]
        public async Task<ActionResult<Tapluyen>> PostTapluyen(Tapluyen tapluyen)
        {
            try
            {
                int newId = await _tapLuyenRepos.Create(tapluyen);

                return CreatedAtAction("GetTapluyen", new { id = newId }, tapluyen);
            }
            catch
            {

                return Problem();
            }
        }

        // DELETE: api/Tapluyens/5
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTapluyen(int id)
        {
            try
            {
                var isSuccess = await _tapLuyenRepos.Delete(id);
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

        private bool TapluyenExists(int id)
        {
            try
            {
                return _tapLuyenRepos.GetById(id) != null;

            }
            catch 
            {

                return false;
            }
        }
    }
}
