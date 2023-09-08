using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Application.Middleware.Transactions;

public static class TransactionsCollectionExtensions
{
    public static IServiceCollection AddUnitOfWorkBehavior(this IServiceCollection services)
    {
        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));

        return services;
    }
}