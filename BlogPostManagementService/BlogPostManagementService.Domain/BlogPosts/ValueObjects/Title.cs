using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.Title;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class Title : SingleValueObject<string>
{
    private const int MinLength = 3;
    private const int MaxLenght = 1000;
    private const string NotAllowedCharacters = "\r\n";

    private Title(string value) : base(value) { }

    public static Result<Title> Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) return Result.Failure<Title>(EmptyTitleFailure.Instance);
        title = title.Trim();

        if (title.Length > MaxLenght)
            return Result.Failure<Title>(new TitleMaxLengthExceededFailure(MaxLenght, title.Length));

        if (title.Length < MinLength)
            return Result.Failure<Title>(new TitleTooShortFailure(MinLength, title.Length));

        if (title.Any(NotAllowedCharacters.Contains))
            return Result.Failure<Title>(NotAllowedTitleCharactersFailure.Instance);

        return Result.Success(new Title(title));
    }
}
