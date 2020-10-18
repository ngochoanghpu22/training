using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Training.Domain.Entities;
using Training.WebApi.Dtos;

namespace Training.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager,
            IConfiguration configuration,
            SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("auth")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);
                if (!result.Succeeded)
                {
                    return BadRequest("Password is incorrect");
                }

                //var roles = await _userManager.GetRolesAsync(user);

                //var permissions = await _permissionService.GetPermissionStringByUserId(user.Id.ToString());

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Role, "Guest"),
                    new Claim("Nationality", "VietNam"),
                    new Claim("Name", "hoang"),
                    new Claim("DateOfBirth", new DateTime(2000,10,28).ToString())
                };
               
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                    _configuration["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return NotFound($"Not found {model.UserName}");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var id = Guid.NewGuid();

            var user = new AppUser {
                Id = id,
                CreatorId = id,
                CreationTime = DateTime.UtcNow,
                FullName = model.FullName, 
                UserName = model.Email, 
                Email = model.Email,
                LockoutEnabled = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                model.Password = null;

                return Ok(model);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("testing")]
        //[AllowAnonymous]
        public async Task<string> Test() 
        {
            return await Task.Run(() => "test api");
        }
    }
}
