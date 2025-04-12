using Microsoft.AspNetCore.Mvc;
using Vulnerabilities.Dtos;
using Vulnerabilities.Services.AuthService;

namespace Vulnerabilities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthService authService,
            ILogger<AuthController> logger
            )
        {
            _authService = authService;
            _logger = logger;            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto login)
        {
            var loginResult = await _authService.AuthenticateUserAsync(login);
            return Ok(loginResult);
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Ok(new { Message = "Logged out successfully" });
        }
    }
}
