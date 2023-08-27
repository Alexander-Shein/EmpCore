using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.BusinessFailures.Commentor;

public class NotAllowedUserNameCharactersFailure : Failure
{
    private const string ErrorCode = "not_allowed_user_name_characters";
    private const string ErrorMessage = "User Name cannot contain newline '\\n' or carriage return '\\r'.";

    public static readonly NotAllowedUserNameCharactersFailure Instance = new();

    private NotAllowedUserNameCharactersFailure() : base(ErrorCode, ErrorMessage) { }
}