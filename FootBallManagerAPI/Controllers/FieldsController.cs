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
    public class FieldsController : ControllerBase
    {
        private readonly IFieldRepository _fieldRepo;

        public FieldsController(IFieldRepository fieldRepository)
        {
            _fieldRepo = fieldRepository;
        }

        // GET: api/Fields
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Field>>> GetFields()
        {
            try
            {
                return Ok(await _fieldRepo.GetAllFieldsAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Fields/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Field>> GetField(int id)
        {
            try
            {

            var field = await _fieldRepo.GetFieldAsync(id);
            return field == null ? NotFound() : Ok(field);
            }
            catch { return BadRequest(); }
        }

        // PUT: api/Fields/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutField(int id, Field @field)
        {
            try
            {

                if(id != field.IdField)
                {
                    return NotFound();
                }
                await _fieldRepo.updateFieldAsync(id, @field);
                return Ok();
            }catch { return BadRequest(); }
        }

        // POST: api/Fields
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Field>> PostField(Field @field)
        {
            try
            {
                var newField = await _fieldRepo.addFieldAsync(@field);
                return newField == null ? NotFound() : Ok(newField);
            }
            catch {
                return BadRequest();
            }
        }

        // DELETE: api/Fields/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            try
            {

            await _fieldRepo.deleteFieldAsync(id);
            return Ok();
            }
            catch { return BadRequest(); }
        }

    }
}
