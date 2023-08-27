using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.BusinessFailures.Commentor;

public class EmptyUserNameFailure : Failure
{
    private const string ErrorCode = "empty_user_name";
    private static readonly string ErrorMessage = "User name must not be empty.";

    public static readonly EmptyUserNameFailure Instance = new();

    private EmptyUserNameFailure() : base(ErrorCode, ErrorMessage) { }
}
