namespace EmpCore.Infrastructure.MessageBus;

public interface IMessageBus
{
    public Task PublishAsync<T>(T applicationEvent, CancellationToken ct = default);
}
