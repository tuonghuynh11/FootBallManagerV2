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
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepo;

        public ServicesController(IServiceRepository repo)
        {
            _serviceRepo = repo;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _serviceRepo.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            try
            {
                var service = await _serviceRepo.GetById(id);

                if (service == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = service,

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateService(int id, Service service)
        {
            try
            {
                if (id != service.IdService)
                {
                    return BadRequest();
                }

                try
                {
                    await _serviceRepo.Update(service);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            try
            {
                int newId = await _serviceRepo.Create(service);

                return CreatedAtAction("GetService", new { id = newId }, service);
            }
            catch
            {

                return Problem();
            }
        }


        //PATCH
        [HttpPatch("patch/{id}")]
        [Authorize]
        public async Task<IActionResult> PatchThongtingiaidau(int id, JsonPatchDocument serviceModel)
        {
            try
            {
                var isSuccess = await _serviceRepo.Patch(id, serviceModel);

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


        // DELETE: api/Services/5
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                var isSuccess = await _serviceRepo.Delete(id);
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

        private bool ServiceExists(int id)
        {
            return _serviceRepo.GetById(id)!=null;
        }
    }
}
