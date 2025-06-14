using EhsaasHub.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EhsaasHub.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost]
        [Route("LoginByPhone")]
        public async Task<IActionResult> LoginByPhone([FromBody] LoginRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.PhoneNumber == request.Phone);
            if (user == null)
                return Unauthorized("Invalid Phone or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized("Invalid phone or password.");

            // Generate new JWT
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                user = new { user.FullName, user.Email, user.Role }
            });
        }


        //login by Email

        [HttpPost]
        [Route("LoginByEmail")]
        public async Task<IActionResult> LoginByEmail([FromBody] LoginRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized("Invalid email or password.");

            // Generate new JWT
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                user = new { user.FullName, user.Email, user.Role }
            });
        }

        //login by CNIC

        [HttpPost]
        [Route("LoginByCNIC")]
        public async Task<IActionResult> LoginByCNIC([FromBody] LoginRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.CNIC == request.CNIC);
            if (user == null)
                return Unauthorized("Invalid CNIC or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized("Invalid CNIC or password.");

            // Generate new JWT
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                user = new { user.FullName, user.Email, user.Role }
            });
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("uid", user.Id),
                new Claim("role", user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    }
