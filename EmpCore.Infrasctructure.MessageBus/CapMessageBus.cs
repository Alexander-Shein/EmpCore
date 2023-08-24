using DotNetCore.CAP;

namespace EmpCore.Infrastructure.MessageBus.CAP;

public class CapMessageBus : IMessageBus
{
    private readonly ICapPublisher _capPublisher;

    public CapMessageBus(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher ?? throw new ArgumentNullException(nameof(capPublisher));
    }

    public async Task PublishAsync<T>(T applicationEvent, CancellationToken ct = default)
    {
        if (applicationEvent == null) throw new ArgumentNullException(nameof(applicationEvent));

        await _capPublisher.PublishAsync(typeof(T).ToString(), applicationEvent, cancellationToken: ct).ConfigureAwait(false);
    }
}
