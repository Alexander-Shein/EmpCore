using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.Content;

public class ContentTooShortFailure : Failure
{
    private const string ErrorCode = "content_too_short";

    public int MinLength { get; }
    public int ActualLength { get; }

    public ContentTooShortFailure(int minLength, int actualLength) : base(
        ErrorCode,
        $"The length of content must be {minLength} characters or more. You entered {actualLength} characters.")
    {
        MinLength = minLength;
        ActualLength = actualLength;
    }
}
