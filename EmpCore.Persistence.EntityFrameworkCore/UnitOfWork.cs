using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;

namespace EmpCore.Persistence.EntityFrameworkCore;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UnitOfWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
    }

    public async Task<Result> SaveAsync()
    {
        await _applicationDbContext.SaveChangesAsync().ConfigureAwait(false);
        return Result.Success();
    }

    public void Dispose()
    {
        _applicationDbContext.Dispose();
    }
}