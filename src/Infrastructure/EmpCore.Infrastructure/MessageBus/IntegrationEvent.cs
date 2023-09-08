namespace EmpCore.Infrastructure.MessageBus;

public abstract class IntegrationEvent
{
    public string EventName { get; }
    public DateTime RaisedAt { get; }

    protected IntegrationEvent(string eventName) : this(eventName, DateTime.UtcNow) { }
    
    protected IntegrationEvent(string eventName, DateTime raisedAt)
    {
        EventName = eventName;
        RaisedAt = raisedAt;
    }
}
