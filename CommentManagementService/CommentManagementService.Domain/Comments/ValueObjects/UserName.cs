using CommentManagementService.Domain.Comments.BusinessFailures.Commentor;
using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.ValueObjects;

public class UserName : SingleValueObject<string>
{
    private const int MinLength = 2;
    private const int MaxLenght = 100;
    private const string NotAllowedCharacters = "\r\n";

    private UserName(string value) : base(value?.Trim()) { }

    public static Result<UserName> Create(string useName)
    {
        if (string.IsNullOrWhiteSpace(useName)) return Result.Failure<UserName>(EmptyUserNameFailure.Instance);
        useName = useName.Trim();

        if (useName.Length > MaxLenght)
            return Result.Failure<UserName>(new UserNameMaxLengthExceededFailure(MaxLenght, useName.Length));

        if (useName.Length < MinLength)
            return Result.Failure<UserName>(new UserNameTooShortFailure(MinLength, useName.Length));
        
        if (useName.Any(NotAllowedCharacters.Contains))
            return Result.Failure<UserName>(NotAllowedUserNameCharactersFailure.Instance);

        return Result.Success(new UserName(useName));
    }
}