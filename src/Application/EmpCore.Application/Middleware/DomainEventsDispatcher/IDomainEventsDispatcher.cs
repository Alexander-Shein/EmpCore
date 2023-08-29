namespace EmpCore.Application.Middleware.DomainEventsDispatcher;

public interface IDomainEventsDispatcher
{
    Task PublishAsync(CancellationToken ct);
}
