using System.Collections.Concurrent;

namespace EmpCore.Domain;

public abstract class AggregateRoot<IId> : Entity<IId>
    where IId : IComparable<IId>
{
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.Keys.ToList();
    private readonly ConcurrentDictionary<DomainEvent, byte> _domainEvents = new();

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.TryAdd(domainEvent ?? throw new ArgumentNullException(nameof(domainEvent)), 0);
    }

    public void ClearDomainEvents ()
    {
        _domainEvents.Clear();
    }
}
