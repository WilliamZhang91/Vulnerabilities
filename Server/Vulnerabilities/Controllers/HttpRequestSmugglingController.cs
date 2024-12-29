using Microsoft.AspNetCore.Mvc;

namespace Vulnerabilities.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class HttpRequestSmugglingController : Controller
    {
        private readonly ILogger<HttpRequestSmugglingController> _logger;

        public HttpRequestSmugglingController(ILogger<HttpRequestSmugglingController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Vulnerable")]
        public IActionResult Vulnerable([FromBody] string text)
        {
            var contentLength = HttpContext.Request.ContentLength;
            return Ok(contentLength);
        }
    }
}
