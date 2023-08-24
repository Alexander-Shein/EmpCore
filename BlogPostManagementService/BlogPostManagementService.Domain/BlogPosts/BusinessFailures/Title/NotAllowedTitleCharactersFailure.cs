using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.Title;

public class NotAllowedTitleCharactersFailure : Failure
{
    private const string ErrorCode = "not_allowed_title_characters";
    private const string ErrorMessage = "Title cannot contain newline '\\n' or carriage return '\\r'.";

    public static readonly NotAllowedTitleCharactersFailure Instance = new();

    private NotAllowedTitleCharactersFailure() : base(ErrorCode, ErrorMessage) { }
}