using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.Title;

public class TitleTooShortFailure : Failure
{
    private const string ErrorCode = "title_too_short";

    public int MinLength { get; }
    public int ActualLength { get; }

    public TitleTooShortFailure(int minLength, int actualLength) : base(
        ErrorCode,
        $"The length of title must be {minLength} characters or more. You entered {actualLength} characters.")
    {
        MinLength = minLength;
        ActualLength = actualLength;
    }
}
