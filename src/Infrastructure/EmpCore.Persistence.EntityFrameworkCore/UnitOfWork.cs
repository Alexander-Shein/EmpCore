using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;
using MediatR;

namespace EmpCore.Persistence.EntityFrameworkCore;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMediator _mediator;
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext, IMediator mediator)
    {
        _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<Result> SaveAsync(CancellationToken ct)
    {
        var aggregateRoots = _appDbContext.ChangeTracker
            .Entries()
            .Where(x => x.Entity.GetType().GetGenericTypeDefinition() == typeof(AggregateRoot<>))
            .ToList();

        await _appDbContext.SaveChangesAsync(ct).ConfigureAwait(false);

        foreach (dynamic aggregateRoot in aggregateRoots)
        {
            foreach (IReadOnlyList<DomainEvent> domainEvent in aggregateRoot.DomainEvents)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
            
            aggregateRoot.ClearDomainEvents();
        }

        return Result.Ok();
    }

    public void Dispose()
    {
        _appDbContext.Dispose();
    }
}