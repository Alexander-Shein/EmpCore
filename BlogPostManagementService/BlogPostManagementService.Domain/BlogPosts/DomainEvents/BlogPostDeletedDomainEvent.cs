using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.DomainEvents
{
    public class BlogPostDeletedDomainEvent : DomainEvent
    {
        public Guid BlogPostId { get; }
        public Guid AuthorId { get; }

        public BlogPostDeletedDomainEvent(Guid blogPostId, Guid authorId)
        {
            BlogPostId = blogPostId;
            AuthorId = authorId;
        }
    }
}