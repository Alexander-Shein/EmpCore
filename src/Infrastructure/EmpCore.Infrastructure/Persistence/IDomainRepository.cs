using EmpCore.Domain;

namespace EmpCore.Infrastructure.Persistence;

public interface IDomainRepository<T, out TId>
    where T : AggregateRoot<TId>
    where TId : IComparable<TId>
{
}