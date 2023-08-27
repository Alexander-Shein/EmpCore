using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.BusinessFailures.Message;

public class MessageMaxLengthExceededFailure : Failure
{
    private const string ErrorCode = "message_max_length_exceeded";

    public int MaxLength { get; }
    public int ActualLength { get; }

    public MessageMaxLengthExceededFailure(int maxLength, int actualLength) : base(
        ErrorCode,
        $"The length of message must be {maxLength} characters or fewer. You entered {actualLength} characters.")
    {
        MaxLength = maxLength;
        ActualLength = actualLength;
    }
}
