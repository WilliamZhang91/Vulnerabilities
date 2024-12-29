using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vulnerabilities.Dtos;
using Vulnerabilities.Services.UserService;

namespace Vulnerabilities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        private readonly IAntiforgery _antiforgery;

        public AuthController(IUserService userService, ILogger<AuthController> logger, IAntiforgery antiforgery)
        {
            _userService = userService;
            _logger = logger;
            _antiforgery = antiforgery;
        }

        [HttpPost("Login")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Login([FromBody] LoginUserDto login)
        {
            var loginResult = await _userService.LoginUserAsync(login);

            if (loginResult.StatusCode == 200)
            {
                try
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, loginResult.Username),
                        new Claim(ClaimTypes.Authentication, loginResult.Status),
                        new Claim(ClaimTypes.Role, loginResult.Role),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    var tokens = _antiforgery.GetAndStoreTokens(HttpContext);

                    Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions
                    {
                        HttpOnly = false,
                        SameSite = SameSiteMode.None,
                        Secure = true
                    });

                    var response = new LoginResponseDto
                    {
                        id = loginResult.id,
                        Username = loginResult.Username,
                        Role = loginResult.Role,
                        StatusCode = loginResult.StatusCode,
                        Status = loginResult.Status
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
            var id = 3;
            var username = "username";
            var role = "admin";

            if (loggedIn)
            {
                var claims = new[]
                {
                    new Claim("id", id.ToString()),
                    new Claim("username", username),
                    new Claim(ClaimTypes.Role, role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Ok(new
                {
                    Message = "Admin Login successful",
                    Success = true,
                });
            }

            return Unauthorized();
        }
    }
}
