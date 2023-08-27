namespace CommentManagementService.Application.Comments.Queries.SearchComments.DTOs
{
    public class CommentListItemDto
    {
        public long Id { get; set; }
        public CommentorDto Commentor { get; set; }
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IReadOnlyList<CommentListItemDto> Replies { get; set; }
    }
}
