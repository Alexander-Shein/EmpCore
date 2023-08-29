using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using EmpCore.Domain;
using MediatR;

namespace BlogPostManagementService.Domain.BlogPosts.DomainEvents
{
    public class BlogPostPublishedDomainEvent : DomainEvent, INotification
    {
        public Guid BlogPostId { get; }
        public string AuthorId { get; }
        public DateTime PublishDateTime { get; }
        public EmailAddress FeedbackEmailAddress { get; }

        public BlogPostPublishedDomainEvent(
            Guid blogPostId, string authorId, DateTime publishDateTime, EmailAddress feedbackEmailAddress)
        {
            BlogPostId = blogPostId;
            AuthorId = authorId;
            PublishDateTime = publishDateTime;
            FeedbackEmailAddress = feedbackEmailAddress;
        }
    }
}