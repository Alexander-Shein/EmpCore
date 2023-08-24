using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.EmailAddress;

public class EmailAddressMaxLengthExceededFailure : Failure
{
    private const string ErrorCode = "email_address_max_length_exceeded";

    public int MaxLength { get; }
    public int ActualLength { get; }

    public EmailAddressMaxLengthExceededFailure(int maxLength, int actualLength) : base(
        ErrorCode,
        $"The length of email must be {maxLength} characters or fewer. You entered {actualLength} characters.")
    {
        MaxLength = maxLength;
        ActualLength = actualLength;
    }
}
