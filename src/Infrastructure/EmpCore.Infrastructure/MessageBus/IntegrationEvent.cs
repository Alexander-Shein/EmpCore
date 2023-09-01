namespace EmpCore.Infrastructure.MessageBus;

public class IntegrationEvent
{
    public string EventName { get; }
    public DateTime RaisedAt { get; } = DateTime.UtcNow;
}
