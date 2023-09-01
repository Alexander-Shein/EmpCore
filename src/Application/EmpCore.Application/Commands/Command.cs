namespace EmpCore.Application.Commands;

public abstract class Command
{
    public DateTime SentAt { get; } = DateTime.UtcNow;
}