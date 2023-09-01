namespace EmpCore.Domain;

[Serializable]
public abstract class DomainEvent
{
    public DateTime RaisedAt { get; } = DateTime.UtcNow;
}
