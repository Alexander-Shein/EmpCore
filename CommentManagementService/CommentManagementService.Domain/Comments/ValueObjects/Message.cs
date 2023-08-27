using CommentManagementService.Domain.Comments.BusinessFailures.Message;
using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.ValueObjects;

public class Message : SingleValueObject<string>
{
    private const int MinLength = 2;
    private const int MaxLenght = 1000;
    private static readonly IEnumerable<string> BlacklistedWorlds = new List<string> { "Word1", "Word2", "Word3" };

    private Message(string value) : base(value?.Trim()) { }

    public static Result<Message> Create(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return Result.Failure<Message>(EmptyMessageFailure.Instance);
        message = message.Trim();

        if (message.Length > MaxLenght)
            return Result.Failure<Message>(new MessageMaxLengthExceededFailure(MaxLenght, message.Length));

        if (message.Length < MinLength)
            return Result.Failure<Message>(new MessageTooShortFailure(MinLength, message.Length));

        foreach (var blackListedWord in BlacklistedWorlds)
        {
            message = message.Replace(blackListedWord, "***", StringComparison.OrdinalIgnoreCase);
        }

        return Result.Success(new Message(message));
    }
}