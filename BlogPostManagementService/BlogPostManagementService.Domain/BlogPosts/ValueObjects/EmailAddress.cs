using BlogPostManagementService.Domain.BlockPosts.BusinessFailures.EmailAddress;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlockPosts.ValueObjects;

public class EmailAddress : SingleValueObject<string>
{
    private const int MaxLength = 256;

    private EmailAddress(string emailAddess) : base(emailAddess)
    {
    }

    public Result<EmailAddress> Create(string emailAddess)
    {
        if (String.IsNullOrWhiteSpace(emailAddess)) return Result.Failure<EmailAddress>(EmptyEmailAddressFailure.Instance);
        emailAddess = emailAddess.Trim();

        if (emailAddess.Length > MaxLength)
            return Result.Failure<EmailAddress>(new EmailAddressMaxLengthExceededFailure(MaxLength, emailAddess.Length));

        // checks if there is only one '@' character
        // and it's neither the first nor the last character
        var indexAtSign = emailAddess.IndexOf('@');
        if (!(indexAtSign > 0
            && indexAtSign != emailAddess.Length - 1
            && indexAtSign == emailAddess.LastIndexOf('@')))
        {
            return Result.Failure<EmailAddress>(new InvalidEmailAddressFailure(emailAddess));
        }

        return Result.Success(new EmailAddress(emailAddess));
    }
}
