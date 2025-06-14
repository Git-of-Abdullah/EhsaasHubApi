using EhsaasHub.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EhsaasHub.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private static Dictionary<string, string> _otpStore = new(); // For demo

        public ResetPasswordController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // STEP 1: Send OTP to phone if user exists
        [HttpPost("send-reset-otp")]
        public async Task<IActionResult> SendResetOtp([FromBody] PhoneDto request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.Phone);
            if (user == null)
                return NotFound("No user found with this phone number.");

            var otp = new Random().Next(100000, 999999).ToString();
            _otpStore[request.Phone] = otp;

            // Simulate sending SMS
            Console.WriteLine($"[RESET OTP] {otp} sent to {request.Phone}");

            return Ok("OTP sent to your phone.");
        }

        // STEP 2: Verify OTP
        [HttpPost("verify-reset-otp")]
        public IActionResult VerifyResetOtp([FromBody] OtpVerificationDto request)
        {
            if (!_otpStore.ContainsKey(request.Phone))
                return BadRequest("No OTP sent for this number.");

            if (_otpStore[request.Phone] != request.Otp)
                return BadRequest("Invalid OTP.");

            return Ok("OTP verified. You can now reset your password.");
        }

        // STEP 3: Reset password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
        {
            if (!_otpStore.ContainsKey(request.Phone))
                return BadRequest("OTP verification is required before resetting password.");

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.Phone);
            if (user == null)
                return NotFound("User not found.");

            // Remove old password if exists
            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (hasPassword)
                await _userManager.RemovePasswordAsync(user);

            var result = await _userManager.AddPasswordAsync(user, request.NewPassword);

            if (!result.Succeeded)
                return BadRequest("Failed to reset password.");

            // ✅ Optionally remove OTP from memory
            _otpStore.Remove(request.Phone);

            return Ok("Password has been reset successfully.");
        }
    }

    // DTOs
    public class PhoneDto
    {
        public string Phone { get; set; }
    }

    public class OtpVerificationDto
    {
        public string Phone { get; set; }
        public string Otp { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Phone { get; set; }
        public string NewPassword { get; set; }
    }
}
