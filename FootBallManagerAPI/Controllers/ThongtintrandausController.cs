using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using FootBallManagerAPI.Repository;

namespace FootBallManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongtintrandausController : ControllerBase
    {
        private readonly IThongTinTranDauRepository _thongTinTranDauRepos;

        public ThongtintrandausController(IThongTinTranDauRepository repo)
        {
            _thongTinTranDauRepos = repo;
        }

        // GET: api/Thongtintrandaus
        [HttpGet]
        public async Task<IActionResult> GetThongtintrandaus()
        {
            try
            {
               
                return  Ok(new { statusCode= StatusCodes.Status200OK, data = await _thongTinTranDauRepos.GetAll() });
            }
            catch
            {

                return Problem();
            }
          
        }

        // GET: api/Thongtintrandaus/5
        [HttpGet("{idTranDau}")]
        public async Task<ActionResult<Thongtintrandau>> GetThongtintrandau(int idTranDau)
        {
            try
            {
                
                var thongtintrandau = await _thongTinTranDauRepos.GetById(idTranDau);

                if (thongtintrandau == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode= StatusCodes.Status200OK,
                    data = thongtintrandau,
                 
                }) ;
            }
            catch
            {

                return Problem();
            }
        
        }

        // PUT: api/Thongtintrandaus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize]

        public async Task<IActionResult> UpdateThongtintrandau(int id,  Thongtintrandau thongtintrandau)
        {
            try
            {
                if (id != thongtintrandau.Id)
                {
                    return BadRequest();
                }

                try
                {
                    await _thongTinTranDauRepos.Update(thongtintrandau);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongtintrandauExists(id))
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
        [HttpPatch("patch/{id}")]
        [Authorize]
        public async Task<IActionResult> PatchThongtintrandau(int id, JsonPatchDocument thongTinTranDauModel)
        {
            try
            {
                var isSuccess= await _thongTinTranDauRepos.Patch(id, thongTinTranDauModel);

                if (isSuccess)
                {
                    return NoContent();

                }
                else
                {
                    return BadRequest(new {message ="Id not exists"});
                }

            }
            catch 
            {

                return Problem();
            }
           
        }



        // POST: api/Thongtintrandaus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]

        public async Task<ActionResult<Thongtintrandau>> CreateNewThongtintrandau(Thongtintrandau thongtintrandau)
        {
            try
            {
               int newId=await _thongTinTranDauRepos.Create(thongtintrandau);

                return CreatedAtAction("GetThongtintrandau", new { id = newId }, thongtintrandau);
            }
            catch
            {

                return Problem();
            }
          
        }

        // DELETE: api/Thongtintrandaus/5
        [HttpDelete("delete/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteThongtintrandau(int id)
        {
            try
            {
                var isSuccess =await _thongTinTranDauRepos.Delete(id);
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

        private bool ThongtintrandauExists(int id)
        {
            try
            {
                return _thongTinTranDauRepos.GetById(id)!=null;

            }
            catch
            {

                return false;
            }
        }
    }
}
