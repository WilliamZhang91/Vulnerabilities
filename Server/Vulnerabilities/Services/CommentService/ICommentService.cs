using Vulnerabilities.Dtos;
using Vulnerabilities.Models;

namespace Vulnerabilities.Services.CommentService
{
    public interface ICommentService
    {
        Task<List<CommentResponseDto>> GetCommentsById(int id);
        Task<Comment> CreateComment(CreateCommentDto createCommentDto);
    }
}
