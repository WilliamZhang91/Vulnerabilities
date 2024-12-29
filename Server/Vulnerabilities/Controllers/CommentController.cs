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

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentById()
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
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
        public async Task<IActionResult> CreateComment([FromBody] string commentText, DateTime createdAt)
        {
            try
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                if (int.TryParse(idClaim, out var id))
                {

                    var createCommentDto = new CreateCommentDto
                    {
                        CommentText = commentText,
                        CreatedAt = createdAt,
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
    }
}
