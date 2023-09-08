using EmpCore.Domain;
using MediatR;

namespace EmpCore.Application.Commands;

public abstract class Command<TResult> : IRequest<TResult>
    where TResult : Result
{
    public DateTime SentAt { get; } = DateTime.UtcNow;
}