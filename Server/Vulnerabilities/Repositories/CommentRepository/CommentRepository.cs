using Microsoft.EntityFrameworkCore;
using Vulnerabilities.Data;
using Vulnerabilities.Dtos;
using Vulnerabilities.Models;

namespace Vulnerabilities.Repositories.CommentRepository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ICommentRepository> _logger;

        public CommentRepository(ApplicationDbContext context, ILogger<ICommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<CommentResponseDto>> GetCommentsById(int id)
        {
            var comments = await _context.Comments
            .Where(c => c.UserId == id)
            .ToListAsync();

            var CommentDtos = comments.Select(comment => new CommentResponseDto
            {
                CommentText = comment.CommentText,
                CreatedAt = comment.CreatedAt
            }).ToList();

            return CommentDtos;
        }

        public async Task<Comment> CreateComment(CreateCommentDto createCommentDto)
        {
            var comment = new Comment
            {
                CommentText= createCommentDto.CommentText,
                CreatedAt = createCommentDto.CreatedAt,
                UserId = createCommentDto.UserId
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
    }
}
