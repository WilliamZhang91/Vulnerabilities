namespace Vulnerabilities.Dtos
{
    public class CreateCommentDto
    {
        public string CommentText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }

    public class CommentResponseDto
    {
        public string CommentText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
