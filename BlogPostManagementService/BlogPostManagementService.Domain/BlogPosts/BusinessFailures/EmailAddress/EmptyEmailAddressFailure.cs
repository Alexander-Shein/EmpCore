using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlockPosts.BusinessFailures.EmailAddress;

public class EmptyEmailAddressFailure : Failure
{
    private const string ErrorCode = "empty_email_address";
    private static readonly string ErrorMessage = "Email address must not be empty.";

    public static readonly EmptyEmailAddressFailure Instance = new();

    private EmptyEmailAddressFailure() : base(ErrorCode, ErrorMessage) { }
}
