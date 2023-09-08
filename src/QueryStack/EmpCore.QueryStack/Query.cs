using MediatR;

namespace EmpCore.QueryStack;

public abstract class Query<TResponse> : IRequest<TResponse>
{
    public DateTime SentAt { get; } = DateTime.UtcNow;
}