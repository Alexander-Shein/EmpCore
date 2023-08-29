using EmpCore.Domain;
using MediatR;
using EmpCore.Application.Commands;

namespace EmpCore.Application.Middleware.DomainEventsDispatcher;

public class DomainEventsDispatcherPipelineBehavior<TCommand, TResult> : IPipelineBehavior<TCommand, TResult>
where TCommand : Command, IRequest<TResult>
where TResult : Result
{
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    public DomainEventsDispatcherPipelineBehavior(IDomainEventsDispatcher domainEventsDispatcher)
    {
        _domainEventsDispatcher =
            domainEventsDispatcher ?? throw new ArgumentNullException(nameof(domainEventsDispatcher));
    }

    public async Task<TResult> Handle(TCommand command, RequestHandlerDelegate<TResult> next, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));
        if (next == null) throw new ArgumentNullException(nameof(next));

        ct.ThrowIfCancellationRequested();

        var result = await next().ConfigureAwait(false);

        if (result.IsSuccess)
        {
            await _domainEventsDispatcher.PublishAsync(ct).ConfigureAwait(false);
        }

        return result;
    }
}
