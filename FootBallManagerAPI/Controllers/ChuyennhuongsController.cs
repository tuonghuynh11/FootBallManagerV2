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
    public class ChuyennhuongsController : ControllerBase
    {
        private readonly IChuyennhuongRepository _chuyennhuongRepo;

        public ChuyennhuongsController(IChuyennhuongRepository chuyennhuongRepository)
        {
            _chuyennhuongRepo = chuyennhuongRepository;
        }

        // GET: api/Chuyennhuongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chuyennhuong>>> GetChuyennhuongs()
        {
            try
            {
                return Ok(await _chuyennhuongRepo.GetAllChuyennhuongAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Chuyennhuongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chuyennhuong>> GetChuyennhuong(int id)
        {
            try
            {

                var chuyennhuong = await _chuyennhuongRepo.GetChuyennhuongAsync(id);
                return chuyennhuong == null ? NotFound() : Ok(chuyennhuong);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Chuyennhuongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChuyennhuong(int id, Chuyennhuong chuyennhuong)
        {
            try
            {

                if (id != chuyennhuong.Id)
                {
                    return NotFound();
                }
                await _chuyennhuongRepo.updateChuyennhuongAsync(id, chuyennhuong);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Chuyennhuongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chuyennhuong>> PostChuyennhuong(Chuyennhuong chuyennhuong)
        {
            try
            {
                var newChuyennhuong = await _chuyennhuongRepo.addChuyennhuongAsync(chuyennhuong);
                return newChuyennhuong == null ? NotFound() : Ok(newChuyennhuong);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Chuyennhuongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChuyennhuong(int id)
        {
            try
            {

                await _chuyennhuongRepo.deleteChuyennhuongAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

       
    }
}
