namespace EmpCore.Infrastructure.MessageBus;

public interface IMessageBus
{
    public Task PublishAsync<T>(T integrationEvent, CancellationToken ct = default)
        where T : IntegrationEvent;
}
