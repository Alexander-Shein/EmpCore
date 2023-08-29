namespace BlogPostManagementService.Application.BlogPosts.Queries.GetBlogPostById.DTOs
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string AuthorId { get; set; }
        public string FeedbackEmailAddress { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishStatus { get; set; }
        public DateTime? PublishDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IEnumerable<ContentEmbeddedResourceDto> EmbeddedResourses { get; set; }
    }
}
