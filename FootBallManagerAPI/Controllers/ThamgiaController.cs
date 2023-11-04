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
    public class ThamgiaController : ControllerBase
    {
        private readonly IThamGiaRepository _thamGiaRepos;

        public ThamgiaController(IThamGiaRepository repo)
        {
            _thamGiaRepos = repo;
        }

        // GET: api/Thamgia
        [HttpGet]
        public async Task<IActionResult> GetThamgias()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _thamGiaRepos.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/Thamgia/5
        [HttpGet("{idTran}")]
        public async Task<ActionResult<List<Thamgium>>> GetThamgia(int idTran)
        {
            try
            {
                var thamgias = await _thamGiaRepos.GetById(idTran);

                if (thamgias == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = thamgias,

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/Thamgia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/")]
        [Authorize]
        public async Task<IActionResult> UpdateThamgia( Thamgium thamgium)
        {
            try
            {
                
                try
                {
                    await _thamGiaRepos.Update(thamgium);
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

            }
            catch
            {

                return Problem();
            }
        }


        //PATCH
        [HttpPatch("patch/{idTran}/{idCauThu}")]
        [Authorize]
        public async Task<IActionResult> PatchThamGia(int idTran, int idCauThu, JsonPatchDocument thamGiaModel)
        {
            try
            {
                var isSuccess = await _thamGiaRepos.Patch(idTran,idCauThu,thamGiaModel);

                if (isSuccess)
                {
                    return NoContent();

                }
                else
                {
                    return BadRequest();
                }

            }
            catch
            {

                return Problem();
            }

        }



        // POST: api/Thamgia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]
        public async Task<ActionResult<Thamgium>> PostThamgia(Thamgium thamgium)
        {
            try
            {
                int newId = await _thamGiaRepos.Create(thamgium);

                return CreatedAtAction("GetThamgia", new { id = newId }, thamgium);
            }
            catch
            {

                return Problem();
            }
        }

        // DELETE: api/Thamgia/5
        [HttpDelete("delete/{idTran}/{idCauThu}")]
        [Authorize]

        public async Task<IActionResult> DeleteThamgium(int idTran, int idCauThu)
        {
            try
            {
                var isSuccess = await _thamGiaRepos.Delete(idTran,idCauThu);
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
        [HttpPost("exist/{idTran}/{idCauThu}")]
        public async Task<IActionResult> CheckPlayerJoin(int idTran,int idCauThu)
        {
            try
            {
                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    isExist =await _thamGiaRepos.IsPlayerJoin(idTran, idCauThu),
                }); ;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
          
        }
    }
}
