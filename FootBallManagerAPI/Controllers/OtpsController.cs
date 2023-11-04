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
    public class OtpsController : ControllerBase
    {
        private readonly IOtpRepository _otpRepos;

        public OtpsController(IOtpRepository repo)
        {
            _otpRepos = repo;
        }

        // GET: api/Otps
        [HttpGet]
        public async Task<IActionResult> GetOtps()
        {
            try
            {
                return Ok(new { statusCode = StatusCodes.Status200OK, data = await _otpRepos.GetAll() });
            }
            catch
            {

                return Problem();
            }
        }

        // GET: api/Otps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Otp>> GetOtp(int id)
        {
            try
            {
                var otp = await _otpRepos.GetById(id);

                if (otp == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    data = otp,

                });
            }
            catch
            {

                return Problem();
            }
        }

        // PUT: api/Otps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateOtp(int id, Otp otp)
        {
            try
            {
                if (id != otp.Id)
                {
                    return BadRequest();
                }

                try
                {
                    await _otpRepos.Update(otp);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtpExists(id))
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


        //PATCH
        [HttpPatch("patch/{id}")]
        [Authorize]
        public async Task<IActionResult> PatchOtp(int id, JsonPatchDocument otpModel)
        {
            try
            {
                var isSuccess = await _otpRepos.Patch(id, otpModel);

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


        // POST: api/Otps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]
        public async Task<ActionResult<Otp>> PostOtp(Otp otp)
        {
            try
            {
                int newId = await _otpRepos.Create(otp);

                return CreatedAtAction("GetOtp", new { id = newId }, otp);
            }
            catch
            {

                return Problem();
            }
        }

        // DELETE: api/Otps/5
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOtp(int id)
        {
            try
            {
                var isSuccess = await _otpRepos.Delete(id);
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

        private bool OtpExists(int id)
        {
            try
            {
                return _otpRepos.GetById(id) != null;

            }
            catch
            {

                return false;
            }
        }
    }
}
