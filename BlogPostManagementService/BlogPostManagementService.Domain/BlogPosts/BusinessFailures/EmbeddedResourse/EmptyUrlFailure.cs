using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlockPosts.BusinessFailures.EmbeddedResourse;

public class EmptyUrlFailure : Failure
{
    private const string ErrorCode = "empty_embedded_resourse_url";
    private static readonly string ErrorMessage = "Embedded resourse url must not be empty.";

    public static readonly EmptyUrlFailure Instance = new();

    private EmptyUrlFailure() : base(ErrorCode, ErrorMessage) { }
}
