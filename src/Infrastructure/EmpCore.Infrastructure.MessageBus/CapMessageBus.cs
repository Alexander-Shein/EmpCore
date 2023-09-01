using DotNetCore.CAP;

namespace EmpCore.Infrastructure.MessageBus.CAP;

public class CapMessageBus : IMessageBus
{
    private readonly ICapPublisher _capPublisher;

    public CapMessageBus(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher ?? throw new ArgumentNullException(nameof(capPublisher));
    }

    public async Task PublishAsync<T>(T integrationEvent, CancellationToken ct = default)
        where T : IntegrationEvent
    {
        if (integrationEvent == null) throw new ArgumentNullException(nameof(integrationEvent));
        
        if (String.IsNullOrWhiteSpace(integrationEvent.EventName))
        {
            if (integrationEvent.EventName == null) throw new ArgumentNullException(nameof(integrationEvent), "EventName cannot be empty.");
            throw new ArgumentException("EventName cannot be empty.", nameof(integrationEvent));
        }

        await _capPublisher
            .PublishAsync(integrationEvent.EventName, integrationEvent, cancellationToken: ct)
            .ConfigureAwait(false);
    }
}
