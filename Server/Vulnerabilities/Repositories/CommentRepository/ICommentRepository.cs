using Vulnerabilities.Dtos;
using Vulnerabilities.Models;

namespace Vulnerabilities.Repositories.CommentRepository
{
    public interface ICommentRepository
    {
        public Task<List<CommentResponseDto>> GetCommentsById(int id);
        public Task<Comment> CreateComment(CreateCommentDto comment);
    }
}
