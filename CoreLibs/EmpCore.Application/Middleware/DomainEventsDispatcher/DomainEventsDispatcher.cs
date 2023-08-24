using EmpCore.Domain;
using MediatR;
using System.Collections.Concurrent;

namespace EmpCore.Application.Middleware.DomainEventsDispatcher;

public class DomainEventsDispatcher : IDomainEventsHolder, IDomainEventsDispatcher
{
    private readonly IMediator _mediator;
    private readonly ConcurrentDictionary<DomainEvent, byte> _domainEvents = new();

    public DomainEventsDispatcher(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public void AddFrom<T>(AggregateRoot<T> aggregateRoot) where T : IComparable<T>
    {
        if (aggregateRoot == null) throw new ArgumentNullException(nameof(aggregateRoot));

        var events = aggregateRoot.DomainEvents;
        if (!events.Any()) return;

        foreach (var ev in events)
        {
            _domainEvents.TryAdd(ev, 0);
        }

        aggregateRoot.ClearDomainEvents();
    }

    public async Task PublishAsync(CancellationToken ct)
    {
        foreach (var ev in _domainEvents.Keys)
        {
            if (_domainEvents.TryRemove(ev, out _))
            {
                await _mediator.Publish(ev, ct).ConfigureAwait(false);
            }
        }
    }
}
