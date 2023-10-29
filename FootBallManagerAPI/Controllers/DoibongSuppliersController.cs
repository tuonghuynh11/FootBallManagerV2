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
    public class DoibongSuppliersController : ControllerBase
    {
        private readonly IDoiBongSupplierRepository _doibongSupplierRepos;

        public DoibongSuppliersController(IDoiBongSupplierRepository repo)
        {
            _doibongSupplierRepos = repo;
        }


        // GET: api/DoibongSuppliers
        [HttpGet]
        public async Task<IActionResult> GetDoibongsuppliers()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _doibongSupplierRepos.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/DoibongSuppliers/5
        [HttpGet("{idSupplier}/{idDoiBong}")]
        public async Task<ActionResult<Doibongsupplier>> GetDoibongsupplier(int idSupplier, string idDoiBong)
        {
            try
            {
                var doibongSupplier = await _doibongSupplierRepos.GetById(idSupplier,idDoiBong);

                if (doibongSupplier == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = doibongSupplier,

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/DoibongSuppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{idSupplier}/{idDoiBong}")]
        [Authorize]
        public async Task<IActionResult> UpdateDoibongsupplier(int idSupplier, string idDoiBong, Doibongsupplier doibongsupplier)
        {
           

            try
            {
                if ((idSupplier != doibongsupplier.IdSupplier) || (idDoiBong != doibongsupplier.IdDoiBong))
                {
                    return BadRequest();
                }

                try
                {
                    await _doibongSupplierRepos.Update(doibongsupplier);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoibongsupplierExists(idSupplier, idDoiBong))
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
        [HttpPatch("patch/{idSupplier}/{idDoiBong}")]
        [Authorize]
        public async Task<IActionResult> PatchThongtingiaidau(int idSupplier, string idDoiBong, JsonPatchDocument doibongSupplierModel)
        {
            try
            {
                var isSuccess = await _doibongSupplierRepos.Patch(idSupplier,idDoiBong, doibongSupplierModel);

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



        // POST: api/DoibongSuppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]
        public async Task<ActionResult<Doibongsupplier>> PostDoibongsupplier(Doibongsupplier doibongsupplier)
        {
            try
            {
                int newId = await _doibongSupplierRepos.Create(doibongsupplier);

                return CreatedAtAction("GetDoibongsupplier", new {  idSupplier=doibongsupplier.IdSupplier,  idDoiBong= doibongsupplier.IdDoiBong }, doibongsupplier);
            }
            catch
            {

                return Problem();
            }
        }

        // DELETE: api/DoibongSuppliers/5
        [HttpDelete("delete/{idSupplier}/{idDoiBong}")]
        [Authorize]
        public async Task<IActionResult> DeleteDoibongsupplier(int idSupplier, string idDoiBong)
        {
            try
            {
                var isSuccess = await _doibongSupplierRepos.Delete(idSupplier,idDoiBong);
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

        private bool DoibongsupplierExists(int idSupplier, string idDoiBong)
        {
            return _doibongSupplierRepos.GetById(idSupplier,idDoiBong)!=null;
        }
    }
}
