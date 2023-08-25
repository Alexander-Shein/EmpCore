using BlogPostManagementService.Contracts;
using BlogPostManagementService.Domain.BlogPosts.DomainEvents;
using EmpCore.Infrastructure.MessageBus;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.DomainEvents
{
    public class BlogPostDeletedDomainEventHandler : INotificationHandler<BlogPostDeletedDomainEvent>
    {
        private readonly IMessageBus _messageBus;

        public BlogPostDeletedDomainEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
        }

        public async Task Handle(BlogPostDeletedDomainEvent domainEvent, CancellationToken ct)
        {
            if (domainEvent == null) throw new ArgumentNullException(nameof(domainEvent));

            var @event = new BlogPostDeletedEvent(
                domainEvent.BlogPostId,
                domainEvent.AuthorId,
                DateTime.UtcNow);

            await _messageBus.PublishAsync(@event, ct).ConfigureAwait(false);
        }
    }
}
