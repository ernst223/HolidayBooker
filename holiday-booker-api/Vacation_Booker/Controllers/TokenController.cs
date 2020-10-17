using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacation_Booker.Entities;
using Vacation_Booker.Models;

namespace Vacation_Booker.Controllers
{
    [Route("api/auth")]
    public class TokenController : Controller
    {
        private UserManager<User> _userMgr;
        private IPasswordHasher<User> _hasher;
        private IConfiguration _config;

        public TokenController(UserManager<User> userMgr, IPasswordHasher<User> hasher, IConfiguration config)
        {
            _userMgr = userMgr;
            _hasher = hasher;
            _config = config;
        }

        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] Credentials model)
        {
            try
            {
                var user = await _userMgr.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfgsdgrre;n34l5n;sdfgsdfg;dngk34l;wert;wert"));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                           //Ernst
                           //issuer: "http://localhost:55081",
                           //audience: "http://localhost:55081",

                           //Henno
                          // issuer: "http://localhost:55082",
                           //audience: "http://localhost:55082",
                           
                           //Staging
                           issuer: "http://hb.dankospark.co.za/api/",
                           audience: "http://hb.dankospark.co.za/api/",

                           expires: DateTime.Now.AddHours(10),
                           signingCredentials: creds
                           );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            userId = user.Id,
                            name = user.Name,
                            userName = user.UserName,
                        });
                    }
                }
                else
                {
                    //If this user does not exist create the user

                    var defaultUser = await _userMgr.FindByNameAsync("info@holidaybooker.co.za");
                    if (defaultUser == null)
                    {
                        User idu = new User()
                        {
                            UserName = "info@holidaybooker.co.za",
                            Name = "HolidayBooker",
                            Email = "info@holidaybooker.co.za"
                        };
                        await _userMgr.CreateAsync(idu, "#Holidaybooker2008");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest("failed to generate token");
            }
            return BadRequest("failed to generate token");
        }
    }
}
