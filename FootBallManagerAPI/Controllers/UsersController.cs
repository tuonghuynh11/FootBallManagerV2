using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MyWebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using NuGet.Packaging;
using NuGet.Protocol;
using Microsoft.AspNetCore.JsonPatch;

namespace FootBallManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FootBallManagerV2Context _context;
        public readonly AppSettings _appSettings;

        public UsersController(FootBallManagerV2Context context, IOptionsMonitor<AppSettings> options)
        {
            _context = context;
            _appSettings = options.CurrentValue;

        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
         
            try
            {
                if (_context.Users == null)
                {
                    return NotFound();
                }
                return Ok (new { statusCode = StatusCodes.Status200OK, data = await _context.Users.ToListAsync() });

            }
            catch (Exception ex)
            {
                return Problem();

            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                if (_context.Users == null)
                {
                    return NotFound();
                }
                var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, data = user });
            }
            catch 
            {

                return Problem();
            }
         
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
           

            try
            {
                if (id != user.Id)
                {
                    return BadRequest();
                }

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return NoContent();

            }
            catch
            {
                try
                {
                    if (!UserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Problem();
                    }
                }
                catch 
                {

                    return Problem();

                }

            }

        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create/")]
        public async Task<ActionResult<User>> CreateNewUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'FootBallManagerV2Context.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
        // PATCH: api/Users/patch
        [HttpPatch("patch/{id}")]
        [Authorize]
        public async Task<IActionResult> PatchUser(int id, JsonPatchDocument updateUser)
        {
            try
            {
                if (_context.Users == null)
                {
                    return NotFound();
                }
                var user = await _context.Users.Where(u=>u.Id==id).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }
                updateUser.ApplyTo(user);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return Problem();
            }

        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                if (_context.Users == null)
                {
                    return NotFound();
                }
                var user = await _context.Users.Where(u=>u.Id==id).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch 
            {
                return Problem() ;
            }
           
        }

        private bool UserExists(int id)
        {
            try
            {
                return (_context.Users?.Where(e => e.Id == id).FirstOrDefault())!=null;

            }
            catch 
            {

                return false;
            }
        }



        [HttpPatch("resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetObj )
        {
            try
            {
                if (resetObj.NewPassword.IsNullOrEmpty())
                {
                    return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, message = "Reset password is not empty" });
                }
                if (_context.Users == null)
                {
                    return Problem();
                }
                User user = await _context.Users.Where(u => u.Id == resetObj.IdUser).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }
                user.Password = resetObj.NewPassword;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    statusCode = StatusCodes.Status200OK,
                    message = "Change password successfully"

                });
            }
            catch 
            {
                return Problem();
            }
            
        }


        [HttpPost("create/LogIn")]
        public async Task<IActionResult> LogIn(LoginUser login)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == login.UserName && u.Password == login.Password);
            if (user != null)
            {
                Userrole userrole = await _context.Userroles.Where(u=>u.Id==user.Iduserrole).FirstOrDefaultAsync();
                user.IduserroleNavigation= userrole;
                var token = await generateToken(user);
                return Ok(new APIResponse()
                {
                    Success = true,
                    message = "Authentication success",
                    data = token
                });
            }
            return NotFound(new APIResponse()
            {
                Success = false,
                message = "Invalid username/password"
            });
        }

        private async Task<TokenModel> generateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Displayname),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserName", user.Username),
                    new Claim("Id", user.Id.ToString()),
                     new Claim("Role", user.IduserroleNavigation.Role!),
                    new Claim("tokenId", Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha512Signature)

            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = generateRefreshToken();

            //store to database
            var refreshTokenEntity = new Refreshtoken()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                JwtId = token.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                Expired = DateTime.UtcNow.AddMonths(1),

            };
            await _context.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();
            return new TokenModel()
            {
                accessToken = accessToken.ToString(),
                refreshToken = refreshToken
            };
        }

        private string generateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewToken(TokenModel renewToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenvalidateParam = new TokenValidationParameters()
            {
                //Auto generate token
                ValidateIssuer = false,
                ValidateAudience = false,

                //get token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false //No check expire
            };
            try
            {
                //Check 1: AccesToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(
                    renewToken.accessToken,
                    tokenvalidateParam,
                    out var validatedToken);
                //Check 2: Check algorithom
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return Ok(new APIResponse()
                        {
                            Success = false,
                            message = "Invalid Token"
                        });
                    }
                }

                //Check 3: Check accessToken expire
      
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate >= DateTime.UtcNow)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        message = "Access Token has not yet expired"
                    });
                }

                //Check 4: Check refreshToken exists in db
                var storedToken = _context.Refreshtokens.FirstOrDefault(x => x.Token == renewToken.refreshToken);

                if (storedToken == null)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        message = "Refresh Token does not exist"
                    });
                }
                //Check 5: Check refreshToken is used/revoked
                if ((bool)storedToken.IsUsed)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        message = "Refresh Token has been used"
                    });
                }


                if (storedToken.IsRevoked)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        message = "Refresh Token has been revoked"
                    });
                }

                //Check 6: AccessToken id == Jwtid in RefreshToken


                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return Ok(new APIResponse()
                    {
                        Success = false,
                        message = "Token doesn't match"
                    });
                }
                //Update token is used

                storedToken.IsUsed = true;
                storedToken.IsRevoked = true;
                _context.Update(storedToken);

                await _context.SaveChangesAsync();

                //Create New Token
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == storedToken.UserId);
                Userrole userrole = await _context.Userroles.FindAsync(user.Iduserrole);
                user.IduserroleNavigation = userrole;
                var token = await generateToken(user);
                return Ok(new APIResponse()
                {
                    Success = true,
                    message = "Renew Token success",
                    data = token
                });


            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse()
                {
                    Success = false,
                    message = "Someting went wrong",
                    data = ex

                });
            }


        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
           
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0,DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTimeInterval;
        }
    }
}
