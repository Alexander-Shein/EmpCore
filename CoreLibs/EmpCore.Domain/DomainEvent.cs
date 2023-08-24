namespace EmpCore.Domain;

public abstract class DomainEvent
{
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}
