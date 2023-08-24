using BlogPostManagementService.Domain.BlockPosts.ValueObjects;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.DomainEvents
{
    public class BlogPostPublishedDomainEvent : DomainEvent
    {
        public Guid BlogPostId { get; }
        public Guid AuthorId { get; }
        public DateTime PublishDateTime { get; }
        public EmailAddress FeedbackEmailAddress { get; }

        public BlogPostPublishedDomainEvent(
            Guid blogPostId, Guid authorId, DateTime publishDateTime, EmailAddress feedbackEmailAddress)
        {
            BlogPostId = blogPostId;
            AuthorId = authorId;
            PublishDateTime = publishDateTime;
            FeedbackEmailAddress = feedbackEmailAddress;
        }
    }
}