namespace EmpCore.Infrastructure.MessageBus;

public interface IMessageBus
{
    public Task PublishAsync<T>(string eventName, T applicationEvent, CancellationToken ct = default);
}
