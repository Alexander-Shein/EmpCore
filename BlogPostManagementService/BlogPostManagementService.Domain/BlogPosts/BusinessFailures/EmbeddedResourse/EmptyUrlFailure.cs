using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.EmbeddedResourse;

public class EmptyUrlFailure : Failure
{
    private const string ErrorCode = "empty_embedded_resourse_url";
    private static readonly string ErrorMessage = "Embedded resource url must not be empty.";

    public static readonly EmptyUrlFailure Instance = new();

    private EmptyUrlFailure() : base(ErrorCode, ErrorMessage) { }
}
