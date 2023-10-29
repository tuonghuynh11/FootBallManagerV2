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

namespace FootBallManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierServicesController : ControllerBase
    {
        private readonly ISupplierServiceRepository _supplierServiceRepo;

        public SupplierServicesController(ISupplierServiceRepository repo)
        {
           _supplierServiceRepo= repo;
        }

        // GET: api/SupplierServices
        [HttpGet]
        public async Task<IActionResult> GetSupplierservices()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _supplierServiceRepo.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/SupplierServices/5
        [HttpGet("{idService}/{idSupplier}")]
        public async Task<ActionResult<Supplierservice>> GetSupplierservice(int idService, int idSupplier)
        {
            try
            {
                var supplierService = await _supplierServiceRepo.GetById(idService, idSupplier);

                if (supplierService == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = supplierService,

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/SupplierServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{idService}/{idSupplier}")]
        [Authorize]
        public async Task<IActionResult> UpdateSupplierservice(int idService,int idSupplier, Supplierservice supplierservice)
        {
            try
            {
                if (idService != supplierservice.IdService || idSupplier != supplierservice.IdSupplier)
                {
                    return BadRequest();
                }

                try
                {
                    await _supplierServiceRepo.Update(supplierservice);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierserviceExists(idService, idSupplier))
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

        // POST: api/SupplierServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]
        public async Task<ActionResult<Supplierservice>> PostSupplierservice(Supplierservice supplierservice)
        {
            try
            {
                int newId = await _supplierServiceRepo.Create(supplierservice);

                return CreatedAtAction("GetSupplierservice", new {  idService=supplierservice.IdService,  idSupplier=supplierservice.IdSupplier }, supplierservice);
            }
            catch
            {

                return Problem();
            }
        }

        // DELETE: api/SupplierServices/5
        [HttpDelete("delete/{idService}/{idSupplier}")]
        public async Task<IActionResult> DeleteSupplierservice(int idService,int idSupplier)
        {
            try
            {
                var isSuccess = await _supplierServiceRepo.Delete(idService, idSupplier);
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

        private bool SupplierserviceExists(int idService, int idSupplier)
        {
            return _supplierServiceRepo.GetById(idService,idSupplier)!=null;
        }
    }
}
