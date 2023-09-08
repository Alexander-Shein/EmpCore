using EmpCore.Application.Commands;
using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;
using MediatR;

namespace EmpCore.Application.Middleware.Transactions;

public class TransactionPipelineBehavior<TCommand, TResult> : IPipelineBehavior<TCommand, TResult>
    where TCommand : Command<TResult>, IRequest<TResult>
    where TResult : Result
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionPipelineBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<TResult> Handle(TCommand command, RequestHandlerDelegate<TResult> next, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));
        if (next == null) throw new ArgumentNullException(nameof(next));

        ct.ThrowIfCancellationRequested();
        
        var result = await next().ConfigureAwait(false);
        if (result.IsFailure) return result;

        await _unitOfWork.SaveAsync(ct).ConfigureAwait(false);
        
        return result;
    }
}