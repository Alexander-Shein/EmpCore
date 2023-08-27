namespace BlogPostManagementService.Contracts
{
    public class BlogPostDeletedEvent : EventBase
    {
        public const string EventName = "BlogPostManagement.BlogPostDeleted";
        public Guid BlogPostId { get; }
        public Guid AuthorId { get; }

        public BlogPostDeletedEvent(Guid blogPostId, Guid authorId, DateTime createdAt) : base (createdAt)
        {
            BlogPostId = blogPostId;
            AuthorId = authorId;
        }        
    }
}