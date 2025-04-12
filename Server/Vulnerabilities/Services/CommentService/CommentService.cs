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

        public void UpdateComment(int number)
        {
            try
            {
                _commentRepository.UpdateComment(number);
            }
            catch (Exception ex) 
            {
                _logger.LogError("error thrown");
                throw new Exception(ex.Message);
            }
        }
    }
}
