using BlogPostManagementService.Contracts;
using BlogPostManagementService.Domain.BlogPosts.DomainEvents;
using EmpCore.Infrastructure.MessageBus;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.DomainEvents
{
    public class BlogPostPublishedDomainEventHandler : INotificationHandler<BlogPostPublishedDomainEvent>
    {
        private readonly IMessageBus _messageBus;

        public BlogPostPublishedDomainEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
        }

        public async Task Handle(BlogPostPublishedDomainEvent domainEvent, CancellationToken ct)
        {
            if (domainEvent == null) throw new ArgumentNullException(nameof(domainEvent));

            var @event = new BlogPostPublishedEvent(
                domainEvent.BlogPostId,
                domainEvent.AuthorId,
                domainEvent.PublishDateTime,
                domainEvent.FeedbackEmailAddress,
                DateTime.UtcNow);

            await _messageBus.PublishAsync(@event, ct).ConfigureAwait(false);
        }
    }
}
