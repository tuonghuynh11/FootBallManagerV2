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
    public class DoihinhchinhsController : ControllerBase
    {
        private readonly IDoihinhchinhRepository _doihinhRepo;

        public DoihinhchinhsController(IDoihinhchinhRepository doihinhchinhRepository)
        {
            _doihinhRepo = doihinhchinhRepository;
        }

        // GET: api/Doihinhchinhs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doihinhchinh>>> GetDoihinhchinhs()
        {
            try
            {
                return Ok(await _doihinhRepo.GetAllDoihinhAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Doihinhchinhs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doihinhchinh>> GetDoihinhchinh(string idDoibong, int idCauthu)
        {
            var doihinh = await _doihinhRepo.GetDoihinhAsync(idDoibong, idCauthu);
            return doihinh == null ? NotFound() : Ok(doihinh);
        }

        // PUT: api/Doihinhchinhs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoihinhchinh(string idDoibong, int idCauthu, Doihinhchinh doihinhchinh)
        {
            if (idDoibong != doihinhchinh.Iddoibong || idCauthu != doihinhchinh.Idcauthu)
            {
                return NotFound();
            }
            await _doihinhRepo.updateDoihinhAsync(idDoibong, idCauthu, doihinhchinh);
            return Ok();
        }

        // POST: api/Doihinhchinhs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doihinhchinh>> PostDoihinhchinh(Doihinhchinh doihinhchinh)
        {
            try
            {
                var newDoihinhId = await _doihinhRepo.addDoihinhAsync(doihinhchinh);
                return newDoihinhId == null ? NotFound() : Ok(newDoihinhId);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Doihinhchinhs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoihinhchinh(string idDoibong, int idCauthu)
        {
            await _doihinhRepo.deleteDoihinhAsync(idDoibong, idCauthu);
            return Ok();
        }

    }
}
