using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project_WebApi.DTO;
using Project_WebApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> _userManager,IConfiguration _config)
        {
            this.userManager = _userManager;
            this.config = _config;
        }

        //creat Account


        [HttpPost("register")]
        public async Task<IActionResult> Registeration(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = registerDTO.UserName;
                applicationUser.Email = registerDTO.Email;
                IdentityResult result = await userManager.CreateAsync(applicationUser, registerDTO.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Added Succ");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            
                return BadRequest(ModelState);
        }

        //Login

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await userManager.FindByNameAsync(loginDTO.UserName);
                if (applicationUser != null)
                {
                    bool found = await userManager.CheckPasswordAsync(applicationUser, loginDTO.Password);
                    if (found)
                    {

                        //Claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, applicationUser.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, applicationUser.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        //get role
                        var roles = await userManager.GetRolesAsync(applicationUser);
                        foreach (var itemRole in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, itemRole));
                        }
                        SecurityKey securityKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));



                        SigningCredentials signincred =
                             new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        //Create token
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],//url web api
                            audience: config["JWT:ValidAudiance"],//url consumer angular
                            claims: claims,
                            expires: DateTime.Now.AddHours(4),
                            signingCredentials: signincred
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo
                        });
                    }

                }

                return Unauthorized();
            }

            return Unauthorized();
        }








        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(LoginDTO UL_DTO)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        check and creat Token
        //        ApplicationUser user = await userManager.FindByNameAsync(UL_DTO.UserName);
        //        if (user != null)
        //        {
        //            bool found = await userManager.CheckPasswordAsync(user, UL_DTO.Password);
        //            if (found)
        //            {
        //                climas Token
        //                var claims = new List<Claim>();
        //                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
        //                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        //                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


        //                climas Role
        //                var roles = await userManager.GetRolesAsync(user);
        //                foreach (var itemRole in roles)
        //                {
        //                    claims.Add(new Claim(ClaimTypes.Role, itemRole));
        //                }



        //                SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
        //                SigningCredentials signincred = new SigningCredentials(
        //                  securityKey, SecurityAlgorithms.HmacSha256
        //                    );
        //                creat Token
        //                JwtSecurityToken token = new JwtSecurityToken(
        //                    issuer: config["JWT:ValidIssuer"],
        //                    audience: config["JWT:ValidAudiance"],
        //                    claims: claims,
        //                    expires: DateTime.Now.AddHours(1),
        //                    signingCredentials: signincred
        //                    );
        //                return Ok(new
        //                {
        //                    token = new JwtSecurityTokenHandler().WriteToken(token),
        //                    expiration = token.ValidTo
        //                });
        //            }
        //            return Unauthorized();

        //        }
        //        return Unauthorized();
        //    }
        //    return Unauthorized();




        //}

    }


}
