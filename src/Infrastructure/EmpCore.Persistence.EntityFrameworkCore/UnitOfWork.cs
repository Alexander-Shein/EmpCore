using System.Collections.Concurrent;
using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;
using MediatR;

namespace EmpCore.Persistence.EntityFrameworkCore;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMediator _mediator;
    private readonly AppDbContext _appDbContext;

    private readonly ConcurrentDictionary<DomainEvent, byte> _sentDomainEvents = new();
    
    private readonly Type _aggregateRootBaseType = typeof(AggregateRoot<>);

    public UnitOfWork(AppDbContext appDbContext, IMediator mediator)
    {
        _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<Result> SaveAsync(CancellationToken ct)
    {
        var aggregateRoots = _appDbContext.ChangeTracker
            .Entries()
            .Where(x => IsAssignableToGenericType(x.Entity.GetType(), _aggregateRootBaseType))
            .Select(x => x.Entity)
            .ToList();

        await _appDbContext.SaveChangesAsync(ct).ConfigureAwait(false);

        foreach (dynamic aggregateRoot in aggregateRoots)
        {
            foreach (var domainEvent in aggregateRoot.DomainEvents)
            {
                if (!_sentDomainEvents.TryAdd(domainEvent, 0)) continue;
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }

        return Result.Ok();
    }

    public void Dispose()
    {
        _appDbContext.Dispose();
    }

    private static bool IsAssignableToGenericType(Type givenType, Type genericType)
    {
        var interfaceTypes = givenType.GetInterfaces();

        foreach (var it in interfaceTypes)
        {
            if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                return true;
        }

        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            return true;

        Type baseType = givenType.BaseType;
        if (baseType == null) return false;

        return IsAssignableToGenericType(baseType, genericType);
    }
}