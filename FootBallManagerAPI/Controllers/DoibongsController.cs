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
    public class DoibongsController : ControllerBase
    {
        private readonly IDoibongRepository _doibongRepo;

        public DoibongsController(IDoibongRepository doibongRepository)
        {
            _doibongRepo = doibongRepository;
        }

        // GET: api/Doibongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doibong>>> GetDoibongs()
        {
            try
            {
                return Ok(await _doibongRepo.GetAllDoibongAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Doibongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doibong>> GetDoibong(string id)
        {
            try
            {

                var doibong = await _doibongRepo.GetDoibongAsync(id);
                return doibong == null ? NotFound() : Ok(doibong);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Doibongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoibong(string id, Doibong doibong)
        {
            try
            {

                if (id != doibong.Id)
                {
                    return NotFound();
                }
                await _doibongRepo.updateDoibongAsync(id, doibong);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Doibongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doibong>> PostDoibong(Doibong doibong)
        {
            try
            {
                var newDoibong = await _doibongRepo.addDoibongAsync(doibong);
                return newDoibong == null ? NotFound() : Ok(newDoibong);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Doibongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoibong(string id)
        {
            try
            {

                await _doibongRepo.deleteDoibongAsync(id);
                return Ok();
            }
            catch { return BadRequest(); }
        }
    }
}
