using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Vulnerabilities.Dtos;
using Vulnerabilities.Services.UserService;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Vulnerabilities.Services.TokenService;

namespace Vulnerabilities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        private readonly ITokenService _tokenService;

        public AuthController(
            IUserService userService, 
            ILogger<AuthController> logger,
            ITokenService tokenService
            )
        {
            _userService = userService;
            _logger = logger;
            _tokenService = tokenService;
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto login)
        {
            var loginResult = await _userService.LoginUserAsync(login);

            if (loginResult.StatusCode == 200)
            {
                try
                {
                    var jwtToken = _tokenService.GenerateJwtToken(loginResult.Id);

                    var response = new LoginResponseDto
                    {
                        Username = loginResult.Username,
                        Role = loginResult.Role,
                        StatusCode = loginResult.StatusCode,
                        Status = loginResult.Status,
                        access_token = jwtToken
                    };

                    return Ok(response);

                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Something went wrong: " + ex.Message);
                    return BadRequest("Login failed");
                }
            }

            return Unauthorized();
        }

        [HttpPost("Login/Admin")]
        public async Task<IActionResult> LoginAdmin()
        {
            var loggedIn = true;

            if (loggedIn)
            {

                return Ok(new
                {
                    Message = "Admin Login successful",
                    Success = true,
                });
            }

            return Unauthorized();
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {

            return Ok(new { Message = "Logged out successfully" });
        }
    }
}
