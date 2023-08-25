namespace BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts.DTOs
{
    public class BlogPostListItemDto
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public DateTime? PublishDateTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
