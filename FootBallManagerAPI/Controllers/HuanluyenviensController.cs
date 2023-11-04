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
    public class HuanluyenviensController : ControllerBase
    {
        private readonly IHuanluyenvienRepository _hlvRepo;

        public HuanluyenviensController(IHuanluyenvienRepository huanluyenvienRepository)
        {
            _hlvRepo = huanluyenvienRepository;
        }

        // GET: api/Huanluyenviens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Huanluyenvien>>> GetHuanluyenviens()
        {
            try
            {
                return Ok(await _hlvRepo.GetAllHlvAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Huanluyenviens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Huanluyenvien>> GetHuanluyenvien(int id)
        {
            try
            {

                var hlv = await _hlvRepo.GetHuanluyenvienAsync(id);
                return hlv == null ? NotFound() : Ok(hlv);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Huanluyenviens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHuanluyenvien(int id, Huanluyenvien huanluyenvien)
        {
            try
            {

                if (id != huanluyenvien.Id)
                {
                    return NotFound();
                }
                await _hlvRepo.updateHlvAsync(id, huanluyenvien);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Huanluyenviens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Huanluyenvien>> PostHuanluyenvien(Huanluyenvien huanluyenvien)
        {
            try
            {
                var newHlv = await _hlvRepo.addHlvAsync(huanluyenvien);
                return newHlv == null ? NotFound() : Ok(newHlv);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Huanluyenviens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHuanluyenvien(int id)
        {
            try
            {

                await _hlvRepo.deleteHlvAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
