using EmpCore.Domain;

namespace EmpCore.Infrastructure.Persistence;

public interface IUnitOfWork : IDisposable
{
    Task<Result> SaveAsync(CancellationToken cancellationToken);
}