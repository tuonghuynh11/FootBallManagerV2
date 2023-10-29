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
    public class QuoctichesController : ControllerBase
    {
        private readonly IQuocTichRepository _quoctichRepo;

        public QuoctichesController(IQuocTichRepository repo)
        {
            _quoctichRepo = repo;
        }


        // GET: api/Quoctiches
        [HttpGet]
        public async Task<IActionResult> GetQuoctiches()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _quoctichRepo.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/Quoctiches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quoctich>> GetQuoctich(int id)
        {
            try
            {
                var quoctich = await _quoctichRepo.GetById(id);

                if (quoctich == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = quoctich,

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/Quoctiches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateQuoctich(int id, Quoctich quoctich)
        {
            try
            {
                if (id != quoctich.Id)
                {
                    return BadRequest();
                }

                try
                {
                    await _quoctichRepo.Update(quoctich);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoctichExists(id))
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
        public async Task<IActionResult> PatchThongtingiaidau(int id, JsonPatchDocument quoctichModel)
        {
            try
            {
                var isSuccess = await _quoctichRepo.Patch(id, quoctichModel);

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



        // POST: api/Quoctiches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]
        public async Task<ActionResult<Quoctich>> PostQuoctich(Quoctich quoctich)
        {
            try
            {
                int newId = await _quoctichRepo.Create(quoctich);

                return CreatedAtAction("GetQuoctich", new { id = newId }, quoctich);
            }
            catch
            {

                return Problem();
            }
        }

        // DELETE: api/Quoctiches/5
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteQuoctich(int id)
        {
            try
            {
                var isSuccess = await _quoctichRepo.Delete(id);
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

        private bool QuoctichExists(int id)
        {
            return _quoctichRepo.GetById(id) != null;
        }
    }
}
