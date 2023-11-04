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
    public class CauthusController : ControllerBase
    {
        private readonly ICauthuRepository _cauthuRepo;

        public CauthusController(ICauthuRepository cauthuRepository)
        {
            _cauthuRepo = cauthuRepository;
        }

        // GET: api/Cauthus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cauthu>>> GetCauthus()
        {
            try
            {
                return Ok(await _cauthuRepo.GetAllCauthuAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Cauthus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cauthu>> GetCauthu(int id)
        {
            try
            {
                var cauthu = await _cauthuRepo.getCauthuByIdAsync(id);
                return cauthu == null ? NotFound() : Ok(cauthu);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Cauthus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCauthu(int id, Cauthu cauthu)
        {
            try
            {
                if (id != cauthu.Id)
                {
                    return NotFound();
                }
                await _cauthuRepo.updateCauthuAsync(id, cauthu);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Cauthus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cauthu>> PostCauthu(Cauthu cauthu)
        {
            try
            {
                var newCauthu = await _cauthuRepo.addCauthuAsync(cauthu);
                return newCauthu == null ? NotFound() : Ok(newCauthu);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        // DELETE: api/Cauthus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCauthu(int id)
        {
            try
            {
                await _cauthuRepo.deleteCauthuAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
