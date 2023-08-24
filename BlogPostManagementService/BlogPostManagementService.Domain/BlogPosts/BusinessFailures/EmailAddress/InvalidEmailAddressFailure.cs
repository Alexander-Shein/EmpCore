using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.EmailAddress;

public class InvalidEmailAddressFailure : Failure
{
    private const string ErrorCode = "invalid_email_address";
    private const string ErrorMessage = "Email address has invalid format.";

    public string Value { get; }

    public InvalidEmailAddressFailure(string value) : base(ErrorCode, ErrorMessage)
    {
        Value = value;
    }
}
