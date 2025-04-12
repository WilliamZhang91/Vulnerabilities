using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vulnerabilities.Dtos;
using Vulnerabilities.Services.CommentService;

namespace Vulnerabilities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentService commentService, ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCommentById()
        {
            try
            {
                var idClaim = User.FindFirst("UserId")?.Value;
                _logger.LogInformation(idClaim);

                if (int.TryParse(idClaim, out var id))
                {
                    var comments = await _commentService.GetCommentsById(id);
                    return Ok(comments);
                }
                else
                {
                    return BadRequest("Invalid or missing id");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] string commentText)
        {
            try
            {
                var idClaim = User.FindFirst("UserId")?.Value;
                _logger.LogInformation(idClaim);
                if (int.TryParse(idClaim, out var id))
                {

                    var createCommentDto = new CreateCommentDto
                    {
                        CommentText = commentText,
                        CreatedAt = DateTime.UtcNow,
                        UserId = id
                    };

                    var newComment = await _commentService.CreateComment(createCommentDto);

                    var commentRespoonse = new CommentResponseDto
                    {
                        CommentText = createCommentDto.CommentText,
                        CreatedAt = createCommentDto.CreatedAt
                    };

                    return Ok(commentRespoonse);
                }
                else
                {
                    return BadRequest("Could not create comment");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update")]
        public IActionResult UpdateCommnet()
        {
            int number = 0;

            try
            {
                _commentService.UpdateComment(number);
                return Ok("Comment updated successfully");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Error InvalidOperationException: {ex.Message}");
                return StatusCode(500, new { InvalidOperationException = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                return StatusCode(500, new { GeneralException = ex.Message });
            }
        }
    }
}
