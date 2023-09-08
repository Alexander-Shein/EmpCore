using MediatR;

namespace EmpCore.Domain;

[Serializable]
public abstract class DomainEvent : INotification
{
    public DateTime RaisedAt { get; } = DateTime.UtcNow;
}
