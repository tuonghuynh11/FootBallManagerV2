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
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepos;

        public SuppliersController(ISupplierRepository repo)
        {
            _supplierRepos = repo;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _supplierRepos.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            try
            {
                var supplier = await _supplierRepos.GetById(id);

                if (supplier == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = supplier,
                    footBallTeamsUnCooperate = await _supplierRepos.GetUnCooperateFootBallTeams(id)

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateSupplier(int id, Supplier supplier)
        {
            try
            {
                if (id != supplier.IdSupplier)
                {
                    return BadRequest();
                }

                try
                {
                    await _supplierRepos.Update(supplier);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(id))
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
        public async Task<IActionResult> PatchThongtingiaidau(int id, JsonPatchDocument supplierModel)
        {
            try
            {
                var isSuccess = await _supplierRepos.Patch(id, supplierModel);

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




        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
        {
            try
            {
                int newId = await _supplierRepos.Create(supplier);

                return CreatedAtAction("GetSupplier", new { id = newId }, supplier);
            }
            catch
            {

                return Problem();
            }
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            try
            {
                var isSuccess = await _supplierRepos.Delete(id);
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

        private bool SupplierExists(int id)
        {
            return _supplierRepos.GetById(id)!=null;
        }
    }
}
