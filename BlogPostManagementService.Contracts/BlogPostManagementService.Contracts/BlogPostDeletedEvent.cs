namespace BlogPostManagementService.Contracts
{
    public class BlogPostDeletedEvent : EventBase
    {
        public Guid BlogPostId { get; }
        public Guid AuthorId { get; }

        public BlogPostDeletedEvent(Guid blogPostId, Guid authorId, DateTime createdAt) : base (createdAt)
        {
            BlogPostId = blogPostId;
            AuthorId = authorId;
        }        
    }
}