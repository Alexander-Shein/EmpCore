using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.BusinessFailures.Commentor;

public class UserNameMaxLengthExceededFailure : Failure
{
    private const string ErrorCode = "user_name_max_length_exceeded";

    public int MaxLength { get; }
    public int ActualLength { get; }

    public UserNameMaxLengthExceededFailure(int maxLength, int actualLength) : base(
        ErrorCode,
        $"The length of user name must be {maxLength} characters or fewer. You entered {actualLength} characters.")
    {
        MaxLength = maxLength;
        ActualLength = actualLength;
    }
}
