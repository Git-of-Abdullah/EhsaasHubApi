using EhsaasHub.Models.AuthModels;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public class SignupController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private static Dictionary<string, string> _otpStore;

        //constructor for Dependency Injection
        public SignupController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _config = config;
            _userManager = userManager;
        }
        [HttpPost("sendOtp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequest request)
        {
            var userExists = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.Phone);
            if (userExists != null) {
                return BadRequest("User already exists.");
            }

            var otp = new Random().Next(100000, 999999).ToString();
            _otpStore = new Dictionary<string, string>();
            _otpStore[request.Phone] = otp;


            //   sending SMS (replace with Twilio or other provider)
            Console.WriteLine($"[OTP SENT] {otp} sent to {request.Phone}");


            return Ok(new {message = "OTP Sent Successfully"});
        }

        [HttpPost("verifyotp/register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request) {

            //1. check if the user lready exists (phone)
            var userExists = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.Phone);
            if (userExists != null) { 
            return BadRequest("User Already Exists");
            }
            // 2. Check if email already exists
            var userExistsByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userExistsByEmail != null)
            {
                return BadRequest("A user with this email already exists.");
            }

            // 3. Check if CNIC already exists
            var userExistsByCnic = await _userManager.Users
                .FirstOrDefaultAsync(u => u.CNIC == request.CNIC);
            if (userExistsByCnic != null)
            {
                return BadRequest("A user with this CNIC already exists.");
            }
            //verify otp
            if (!_otpStore.ContainsKey(request.Phone) || _otpStore[request.Phone] != request.Otp)
                return BadRequest("Invalid or expired OTP.");


            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.Phone,
                FullName = request.FullName,
                CNIC = request.CNIC,
                Location = request.Location,
                Role = request.Role,
                LanguagePreference = request.LanguagePreference,
                ProfileImageUrl = request.ProfileImageUrl
            };

             var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Failed To Create Usser");
            }

            //claim addition
            await _userManager.AddClaimAsync(user, new Claim("role", request.Role));

            //JWT issueance
            var token = GenerateJwtToken(user);

            // Remove OTP from store
            _otpStore.Remove(request.Phone);

            return Ok(new
            {
                token,
                user = new { user.FullName, user.Email, user.Role }
            });


            
        }
        //JWT Generator
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
