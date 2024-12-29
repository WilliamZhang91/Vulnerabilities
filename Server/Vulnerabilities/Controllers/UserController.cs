using Microsoft.AspNetCore.Mvc;
using Vulnerabilities.Services.UserService;
using Vulnerabilities.Models;
using Vulnerabilities.Dtos;

namespace Vulnerabilities.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto user)
        {
            try
            {
                var newUser = await _userService.CreateUserAsync(user);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Could not create account");
            }
        }
    }
}
