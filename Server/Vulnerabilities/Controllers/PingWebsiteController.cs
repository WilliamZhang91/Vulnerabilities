using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        [HttpPost("ping")]
        public async Task<IActionResult> PingVulnerable([FromBody] string request)
        {
            if (User.Identity.IsAuthenticated)
            {
                var antiforgery = HttpContext.RequestServices.GetRequiredService<IAntiforgery>();
                antiforgery.ValidateRequestAsync(HttpContext).GetAwaiter().GetResult();

                if (string.IsNullOrEmpty(request))
                {
                    return BadRequest("Command is required.");
                }

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
                catch (AntiforgeryValidationException ex)
                {
                    var headersDictionary = Request.Headers.ToDictionary(
                        h => h.Key,
                        h => h.Value.ToString()
                    );

                    var errorResponse = new
                    {
                        Error = ex.Message,
                        InnerException = ex.InnerException,
                        Headers = headersDictionary,
                    };

                    return StatusCode(500, errorResponse);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        public class CommandRequest
        {
            public string Command { get; set; } = string.Empty;
        }
    }
}
