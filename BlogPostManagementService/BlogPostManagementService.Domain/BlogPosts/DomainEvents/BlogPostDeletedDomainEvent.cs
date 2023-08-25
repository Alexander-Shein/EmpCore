using EmpCore.Domain;
using MediatR;

namespace BlogPostManagementService.Domain.BlogPosts.DomainEvents
{
    public class BlogPostDeletedDomainEvent : DomainEvent, INotification
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