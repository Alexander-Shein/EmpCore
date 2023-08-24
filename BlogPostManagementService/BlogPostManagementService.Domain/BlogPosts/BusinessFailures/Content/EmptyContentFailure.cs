using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.Content;

public class EmptyContentFailure : Failure
{
    private const string ErrorCode = "empty_content";
    private static readonly string ErrorMessage = "Content must not be empty.";

    public static readonly EmptyContentFailure Instance = new();

    private EmptyContentFailure() : base(ErrorCode, ErrorMessage) { }
}
