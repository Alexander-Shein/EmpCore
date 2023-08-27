using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.BusinessFailures.Message;

public class EmptyMessageFailure : Failure
{
    private const string ErrorCode = "empty_message";
    private static readonly string ErrorMessage = "Message must not be empty.";

    public static readonly EmptyMessageFailure Instance = new();

    private EmptyMessageFailure() : base(ErrorCode, ErrorMessage) { }
}
