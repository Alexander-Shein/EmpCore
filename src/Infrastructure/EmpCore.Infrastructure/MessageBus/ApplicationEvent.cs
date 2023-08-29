namespace EmpCore.Infrastructure.MessageBus;

public class ApplicationEvent
{
    public string EventName { get; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}
