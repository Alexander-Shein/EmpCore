using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.BusinessFailures.Commentor;

public class UserNameTooShortFailure : Failure
{
    private const string ErrorCode = "user_name_too_short";

    public int MinLength { get; }
    public int ActualLength { get; }

    public UserNameTooShortFailure(int minLength, int actualLength) : base(
        ErrorCode,
        $"The length of user name must be {minLength} characters or more. You entered {actualLength} characters.")
    {
        MinLength = minLength;
        ActualLength = actualLength;
    }
}
