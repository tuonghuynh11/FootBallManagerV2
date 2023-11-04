using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootBallManagerAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using FootBallManagerAPI.Repository;

namespace FootBallManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserrolesController : ControllerBase
    {
        private readonly IUserRolesRepository _userRoleRepos;

        public UserrolesController(IUserRolesRepository repo)
        {
            _userRoleRepos = repo;
        }

        // GET: api/Userroles
        [HttpGet]
        public async Task<IActionResult> GetUserroles()
        {
            try
            {
                return  Ok(new { statusCode= StatusCodes.Status200OK, data = await _userRoleRepos.GetAll() });

            }
            catch 
            {

                return Problem();
            }
       
        }

        // GET: api/Userroles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserrole(int id)
        {
            try
            {
              
                var userrole = await _userRoleRepos.GetById(id);

                if (userrole == null)
                {
                    return NotFound();
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, data = userrole });
            }
            catch
            {

                return Problem();
            }
         
        }

        // PUT: api/Userroles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserrole(int id, Userrole userrole)
        {
            try
            {
                if (id != userrole.Id)
                {
                    return BadRequest();
                }


                try
                {
                    await _userRoleRepos.Update(userrole);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserroleExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Problem();
                    }
                }

                return NoContent();
            }
            catch
            {

                return Problem();
            }
           
        }
        // PATCH: api/Users/patch
        [HttpPatch("patch/{id}")]
        [Authorize]
        public async Task<IActionResult> PatchUserRole(int id, JsonPatchDocument updateUserRole)
        {
            try
            {
               
                var userrole = await _userRoleRepos.Patch(id,updateUserRole);
                if (!userrole)
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

        // POST: api/Userroles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        [Authorize]

        public async Task<ActionResult<Userrole>> CreateNewUserrole(Userrole userrole)
        {
            try
            {
                int newId = await _userRoleRepos.Create(userrole);

                return CreatedAtAction("GetUserrole", new { id = newId }, userrole);
            }
            catch
            {

                return Problem();
            }
         
        }

        // DELETE: api/Userroles/5
        [HttpDelete("delete/{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteUserrole(int id)
        {
            try
            {
             
                var isSuccess = await _userRoleRepos.Delete(id);
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

        private bool UserroleExists(int id)
        {
            try
            {
                var a = _userRoleRepos.GetById(id);
                return _userRoleRepos.GetById(id)!=null;

            }
            catch
            {

                return false;
            }
        }
    }
}
