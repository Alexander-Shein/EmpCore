namespace EmpCore.Domain;

public abstract class AggregateRoot<IId> : Entity<IId>
    where IId : IComparable<IId>
{
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.ToList();
    private readonly HashSet<DomainEvent> _domainEvents = new();

    protected void RaiseDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent ?? throw new ArgumentNullException(nameof(domainEvent)));
    }

    public void ClearDomainEvents ()
    {
        _domainEvents.Clear();
    }
}
