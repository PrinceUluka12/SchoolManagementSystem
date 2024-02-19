using Identity.Models.Dto;
using Identity.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/passwordreset")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private readonly IPasswordResetService _passwordResetService;

        public PasswordResetController(IPasswordResetService passwordResetService)
        {
            _passwordResetService = passwordResetService;
        }

        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] PasswordResetRequestDto model)
        {
            var result = await _passwordResetService.RequestPasswordResetAsync(model.username);

            if (result)
                return Ok("Password reset code sent successfully.");
            else
                return BadRequest("Invalid username or user not found.");
        }

        [HttpPost("verify-reset")]
        public async Task<IActionResult> VerifyPasswordReset([FromBody] PasswordResetVerificationDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _passwordResetService.VerifyPasswordResetCodeAsync(model.Username, model.Code, model.NewPassword);

            if (result)
                return Ok("Password reset successful.");
            else
                return BadRequest("Invalid user or code.");
        }

    }
}
