using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

// Command injection
namespace Vulnerabilities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingWebsiteController : Controller
    {

        private readonly ILogger<PingWebsiteController> _logger;

        public PingWebsiteController(ILogger<PingWebsiteController> logger)
        {
            _logger = logger;
        }


        [HttpPost("Ping/Vulnerable")]
        //[Authorize(Roles = "admin")]
        [Authorize]
        public async Task<IActionResult> PingVulnerable([FromBody] string request)
        {
            try
            {
                _logger.LogInformation(request);

                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c ping {request}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    var output = await process.StandardOutput.ReadToEndAsync();
                    var error = await process.StandardError.ReadToEndAsync();

                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        return Ok(new { Output = error });
                    }

                    return Ok(new { Output = output });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "Something went wrong"
                });
            }
        }

        [HttpPost("Ping/Invulnerable")]
        public async Task<IActionResult> PingInvulnerable([FromBody] string request)
        {

            if (!Regex.IsMatch(request, @"^[a-zA-Z0-9.-]+$"))
            {
                return BadRequest(new { error = "Invalid input. Only letters, numbers, dots, and hyphens are allowed." });
            }

            try
            {
                _logger.LogInformation(request);

                var startInfo = new ProcessStartInfo
                {
                    FileName = "ping",
                    Arguments = request,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    var output = await process.StandardOutput.ReadToEndAsync();
                    var error = await process.StandardError.ReadToEndAsync();

                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        return Ok(new { Output = error });
                    }

                    return Ok(new { Output = output });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Ping request failed." });
            }
        }
    }
}

