using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace FootBallManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongtingiaidausController : ControllerBase
    {
        private readonly IThongtinGiaiDauRepository _thongTinGiaiDauRepos;

        public ThongtingiaidausController(IThongtinGiaiDauRepository repo)
        {
            _thongTinGiaiDauRepos = repo;
        }

        // GET: api/Thongtingiaidaus
        [HttpGet]
        public async Task<IActionResult> GetThongtingiaidaus()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _thongTinGiaiDauRepos.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/Thongtingiaidaus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Thongtingiaidau>>> GetThongtingiaidau(int id)
        {
            try
            {
                var thongtingiaidau = await _thongTinGiaiDauRepos.GetById(id);

                if (thongtingiaidau == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = thongtingiaidau,

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/Thongtingiaidaus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{idgiaidau}/{idDoiBong}")]
        [Authorize]
        public async Task<IActionResult> UpdateThongtingiaidau(int idgiaidau,string idDoiBong, Thongtingiaidau thongtingiaidau)
        {
            try
            {
                if (idgiaidau != thongtingiaidau.Idgiaidau || idDoiBong!=thongtingiaidau.Iddoibong)
                {
                    return BadRequest();
                }

                try
                {
                    await _thongTinGiaiDauRepos.Update(thongtingiaidau);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongtingiaidauExists(idgiaidau,idDoiBong))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Problem();
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
        [HttpPatch("patch/{idGiaiDau}/{idDoiBong}")]
        [Authorize]
        public async Task<IActionResult> PatchThongtingiaidau(int idGiaiDau, string idDoiBong, JsonPatchDocument thongTinGiaiDauModel)
        {
            try
            {
                var isSuccess = await _thongTinGiaiDauRepos.Patch(idGiaiDau,idDoiBong, thongTinGiaiDauModel);

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



        // POST: api/Thongtingiaidaus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]
        public async Task<ActionResult<Thongtingiaidau>> PostThongtingiaidau(Thongtingiaidau thongtingiaidau)
        {
            try
            {
                int newId = await _thongTinGiaiDauRepos.Create(thongtingiaidau);

                return CreatedAtAction("GetThongtingiaidau", new { id = newId }, thongtingiaidau);
            }
            catch
            {

                return Problem();
            }
        }

        // DELETE: api/Thongtingiaidaus/5
        [HttpDelete("delete/{idGiaiDau}/{idDoiBong}")]
        [Authorize]
        public async Task<IActionResult> DeleteThongtingiaidau(int idGiaiDau,string idDoiBong)
        {
            try
            {
                var isSuccess = await _thongTinGiaiDauRepos.Delete(idGiaiDau, idDoiBong);
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

        private bool ThongtingiaidauExists(int idGiaiDau,string idDoiBong)
        {
            try
            {
                var giaiDau = _thongTinGiaiDauRepos.GetById(idGiaiDau);
                if (giaiDau != null)
                {
                    var doibong = giaiDau.Result.Where(g=>g.Iddoibong==idDoiBong).FirstOrDefault();
                    return doibong != null;
                }
                else
                {
                    return false;
                }

            }
            catch 
            {

                return false;
            }
        }
    }
}
