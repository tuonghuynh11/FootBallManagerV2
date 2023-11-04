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
    public class DiemsController : ControllerBase
    {
        private readonly IDiemRepository _diemRepo;

        public DiemsController(IDiemRepository diemRepository)
        {
            _diemRepo = diemRepository;
        }

        // GET: api/Diems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diem>>> GetDiems()
        {
            try
            {
                return Ok(await _diemRepo.GetAllDiemAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Diems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diem>> GetDiem(int idGiaidau, string idDoibong)
        {
            try
            {

                var diem = await _diemRepo.GetDiemAsync(idGiaidau, idDoibong);
                return diem == null ? NotFound() : Ok(diem);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Diems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiem(int idGiaidau,string idDoibong, Diem diem)
        {
            try
            {

                if (idGiaidau != diem.Idgiaidau || idDoibong != diem.Iddoibong)
                {
                    return NotFound();
                }
                await _diemRepo.updateDiemAsync(idGiaidau, idDoibong, diem);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Diems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diem>> PostDiem(Diem diem)
        {
            try
            {
                var newDiem = await _diemRepo.addDiemAsync(diem);
                return newDiem == null ? NotFound() : Ok(newDiem);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Diems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiem(int idGiaidau, string idDoibong)
        {
            try
            {

                await _diemRepo.deleteDiemAsync(idGiaidau, idDoibong);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
