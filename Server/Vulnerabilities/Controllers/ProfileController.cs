using Microsoft.AspNetCore.Mvc;
using Vulnerabilities.Services.ProfileService;
using Vulnerabilities.Models;
using Microsoft.AspNetCore.Authorization;
using Vulnerabilities.Dtos;

namespace Vulnerabilities.Controllers
{
    //For elevated privelege
    [Route("api/[Controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        IProfileService _profileService;
        ILogger<ProfileController> _logger;

        public ProfileController(IProfileService profileService, ILogger<ProfileController> logger)
        {
            _profileService = profileService;
            _logger = logger;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> ReadProfileVulnerable([FromQuery] string searchValue)
        {
            try
            {
                var profile = await _profileService.ReadProfileAsync(searchValue);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("CreateProfile")]
        public async Task<IActionResult> CreateProfileAsync([FromBody] Profile profile)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var newProfile = await _profileService.CreateProfileAsync(profile);

                    return Ok(newProfile);
                }

                return BadRequest("Invalid profile");

            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("Vulnerable")]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateProfileDto updateProfileDto)
        {
            try
            {
                var idClaim = User.FindFirst("id")?.Value;

                if (int.TryParse(idClaim, out int id))
                {
                    var updatedProfile = await _profileService.UpdateProfileAsync(id, updateProfileDto);
                    return Ok(updatedProfile);
                }

                return BadRequest("Could not update profile");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
