using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.Content;

public class ContentMaxLengthExceededFailure : Failure
{
    private const string ErrorCode = "content_max_length_exceeded";

    public int MaxLength { get; }
    public int ActualLength { get; }

    public ContentMaxLengthExceededFailure(int maxLength, int actualLength) : base(
        ErrorCode,
        $"The length of content must be {maxLength} characters or fewer. You entered {actualLength} characters.")
    {
        MaxLength = maxLength;
        ActualLength = actualLength;
    }
}
