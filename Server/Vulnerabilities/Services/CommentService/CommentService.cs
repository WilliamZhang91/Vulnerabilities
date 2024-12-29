using Vulnerabilities.Dtos;
using Vulnerabilities.Models;
using Vulnerabilities.Repositories.CommentRepository;

namespace Vulnerabilities.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<CommentResponseDto>> GetCommentsById(int id)
        {
            try
            {
                var comments = await _commentRepository.GetCommentsById(id);
                return comments;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Comment> CreateComment(CreateCommentDto createCommentDto)
        {
            try
            {
                var newComment = await _commentRepository.CreateComment(createCommentDto);
                return newComment;
            }
            catch 
            {
                throw;
            }
        }
    }
}
