using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.Title;

public class TitleMaxLengthExceededFailure : Failure
{
    private const string ErrorCode = "title_max_length_exceeded";

    public int MaxLength { get; }
    public int ActualLength { get; }

    public TitleMaxLengthExceededFailure(int maxLength, int actualLength) : base(
        ErrorCode,
        $"The length of title must be {maxLength} characters or fewer. You entered {actualLength} characters.")
    {
        MaxLength = maxLength;
        ActualLength = actualLength;
    }
}
