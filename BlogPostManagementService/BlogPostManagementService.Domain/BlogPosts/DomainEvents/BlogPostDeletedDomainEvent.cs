using EmpCore.Domain;
using MediatR;

namespace BlogPostManagementService.Domain.BlogPosts.DomainEvents
{
    public class BlogPostDeletedDomainEvent : DomainEvent, INotification
    {
        public Guid BlogPostId { get; }
        public string AuthorId { get; }

        public BlogPostDeletedDomainEvent(Guid blogPostId, string authorId)
        {
            BlogPostId = blogPostId;
            AuthorId = authorId;
        }
    }
}