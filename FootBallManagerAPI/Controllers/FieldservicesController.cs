using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Repositories;

namespace FootBallManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldservicesController : ControllerBase
    {
        private readonly IFieldServiceRepository _fieldserviceRepo;

        public FieldservicesController(IFieldServiceRepository fieldServiceRepository)
        {
            _fieldserviceRepo = fieldServiceRepository;
        }

        // GET: api/Fieldservices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fieldservice>>> GetFieldservices()
        {
            try
            {
                return Ok(await _fieldserviceRepo.GetAllFieldservicesAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Fieldservices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fieldservice>> GetFieldservice(int idField, int idService)
        {
            try
            {

                var fieldservice = await _fieldserviceRepo.GetFieldserviceAsync(idField, idService);
                return fieldservice == null ? NotFound() : Ok(fieldservice);
            }
            catch {  return BadRequest(); }
        }

        // PUT: api/Fieldservices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFieldservice(int idField, int idService, Fieldservice fieldservice)
        {
            try
            {

                if (idField != fieldservice.IdField || idService != fieldservice.IdService)
                {
                    return NotFound();
                }
                await _fieldserviceRepo.updateFieldServiceAsync(idField, idService, fieldservice);
                return Ok();
            }catch { return BadRequest(); }
        }

        // POST: api/Fieldservices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fieldservice>> PostFieldservice(Fieldservice fieldservice)
        {
            try
            {
                var newFieldserviceId = await _fieldserviceRepo.addFieldserviceAsync(fieldservice);
                return newFieldserviceId == null ? NotFound() : Ok(newFieldserviceId);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Fieldservices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFieldservice(int idField, int idService)
        {
            try
            {

                await _fieldserviceRepo.deleteFieldserviceAsync(idField, idService);
                return Ok();
            }
            catch { return BadRequest(); }
        }

    }
}
