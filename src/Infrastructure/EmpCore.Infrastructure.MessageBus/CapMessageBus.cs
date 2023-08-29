using DotNetCore.CAP;

namespace EmpCore.Infrastructure.MessageBus.CAP;

public class CapMessageBus : IMessageBus
{
    private readonly ICapPublisher _capPublisher;

    public CapMessageBus(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher ?? throw new ArgumentNullException(nameof(capPublisher));
    }

    public async Task PublishAsync<T>(string eventName, T applicationEvent, CancellationToken ct = default)
    {
        if (String.IsNullOrWhiteSpace(eventName))
        {
            if (eventName == null) throw new ArgumentNullException(nameof(eventName));
            throw new ArgumentException("Cannot be empty.", nameof(eventName));
        }
        if (applicationEvent == null) throw new ArgumentNullException(nameof(applicationEvent));

        await _capPublisher.PublishAsync(eventName, applicationEvent, cancellationToken: ct).ConfigureAwait(false);
    }
}
