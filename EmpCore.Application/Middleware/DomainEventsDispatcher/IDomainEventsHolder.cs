using EmpCore.Domain;

namespace EmpCore.Application.Middleware.DomainEventsDispatcher;

public interface IDomainEventsHolder
{
    void AddFrom<T>(AggregateRoot<T> aggregateRoot)
        where T : IComparable<T>;
}
