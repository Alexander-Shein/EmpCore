using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.BusinessFailures.Message;

public class MessageTooShortFailure : Failure
{
    private const string ErrorCode = "message_too_short";

    public int MinLength { get; }
    public int ActualLength { get; }

    public MessageTooShortFailure(int minLength, int actualLength) : base(
        ErrorCode,
        $"The length of message must be {minLength} characters or more. You entered {actualLength} characters.")
    {
        MinLength = minLength;
        ActualLength = actualLength;
    }
}
