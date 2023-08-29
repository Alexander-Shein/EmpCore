namespace BlogPostManagementService.Contracts
{
    public class BlogPostDeletedEvent : EventBase
    {
        public const string EventName = "BlogPostManagement.BlogPostDeleted";
        public Guid BlogPostId { get; }
        public string AuthorId { get; }

        public BlogPostDeletedEvent(Guid blogPostId, string authorId, DateTime createdAt) : base (createdAt)
        {
            BlogPostId = blogPostId;
            AuthorId = authorId;
        }        
    }
}