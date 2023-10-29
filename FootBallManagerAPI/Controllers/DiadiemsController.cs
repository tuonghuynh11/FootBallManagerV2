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
    public class DiadiemsController : ControllerBase
    {
        private readonly IDiadiemRepository _diadiemRepo;

        public DiadiemsController(IDiadiemRepository diadiemRepository)
        {
            _diadiemRepo = diadiemRepository;
        }

        // GET: api/Diadiems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diadiem>>> GetDiadiems()
        {
            try
            {
                return Ok(await _diadiemRepo.GetAllDiadiemAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Diadiems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diadiem>> GetDiadiem(int id)
        {
            var diadiem = await _diadiemRepo.GetDiadiemAsync(id);
            return diadiem == null ? NotFound() : Ok(diadiem);
        }

        // PUT: api/Diadiems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiadiem(int id, Diadiem diadiem)
        {
            if (id != diadiem.Id)
            {
                return NotFound();
            }
            await _diadiemRepo.updateDiadiemAsync(id, diadiem);
            return Ok();
        }

        // POST: api/Diadiems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diadiem>> PostDiadiem(Diadiem diadiem)
        {
            try
            {
                var newDiadiemId = await _diadiemRepo.addDiadiemAsync(diadiem);
                var diadiemNew = await _diadiemRepo.GetDiadiemAsync(newDiadiemId);
                return diadiemNew == null ? NotFound() : Ok(diadiemNew);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Diadiems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiadiem(int id)
        {
            await _diadiemRepo.DeleteDiadiemAsync(id);
            return Ok();
        }

    }
}
