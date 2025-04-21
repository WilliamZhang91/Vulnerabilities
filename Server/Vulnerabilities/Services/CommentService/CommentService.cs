using Vulnerabilities.Dtos;
using Vulnerabilities.Models;
using Vulnerabilities.Repositories.CommentRepository;

namespace Vulnerabilities.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<ICommentService> _logger;

        public CommentService(ICommentRepository commentRepository, ILogger<ICommentService> logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }

        public async Task<List<CommentResponseDto>> GetCommentsById(int id)
        {
            var comments = await _commentRepository.GetCommentsById(id);
            return comments;
        }

        public async Task<Comment> CreateComment(CreateCommentDto createCommentDto)
        {

            var newComment = await _commentRepository.CreateComment(createCommentDto);
            return newComment;
        }

        public void UpdateComment(int number)
        {
            _commentRepository.UpdateComment(number);
        }
    }
}
