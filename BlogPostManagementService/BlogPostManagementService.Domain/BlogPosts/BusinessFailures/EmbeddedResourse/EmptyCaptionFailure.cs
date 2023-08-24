using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.EmbeddedResourse;

public class EmptyCaptionFailure : Failure
{
    private const string ErrorCode = "empty_embedded_resourse_caption";
    private static readonly string ErrorMessage = "Embedded resource caption must not be empty.";

    public static readonly EmptyCaptionFailure Instance = new();

    private EmptyCaptionFailure() : base(ErrorCode, ErrorMessage) { }
}