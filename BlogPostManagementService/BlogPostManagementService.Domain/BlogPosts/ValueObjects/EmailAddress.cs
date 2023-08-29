using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.EmailAddress;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class EmailAddress : SingleValueObject<string>
{
    private const int MaxLength = 256;

    private EmailAddress(string value) : base(value)
    {
    }

    public static Result<EmailAddress> Create(string emailAddress)
    {
        if (String.IsNullOrWhiteSpace(emailAddress)) return Result.Failure<EmailAddress>(EmptyEmailAddressFailure.Instance);
        emailAddress = emailAddress.Trim();

        if (emailAddress.Length > MaxLength)
            return Result.Failure<EmailAddress>(new EmailAddressMaxLengthExceededFailure(MaxLength, emailAddress.Length));

        // checks if there is only one '@' character
        // and it's neither the first nor the last character
        var indexAtSign = emailAddress.IndexOf('@');
        if (!(indexAtSign > 0
            && indexAtSign != emailAddress.Length - 1
            && indexAtSign == emailAddress.LastIndexOf('@')))
        {
            return Result.Failure<EmailAddress>(new InvalidEmailAddressFailure(emailAddress));
        }

        return Result.Success(new EmailAddress(emailAddress.ToUpperInvariant()));
    }
}
