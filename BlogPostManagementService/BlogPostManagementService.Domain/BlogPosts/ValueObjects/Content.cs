using BlogPostManagementService.Domain.BlockPosts.BusinessFailures.Content;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlockPosts.ValueObjects;

public class Content : SingleValueObject<string>
{
    private const int MinLength = 1000;
    private const int MaxLenght = 100_000;
    private readonly IEnumerable<string> _blacklistedWorlds = new List<string> { "Word1", "Word2", "Word3" };

    private Content(string content) : base(content) { }

    public Result<Content> Create(string content)
    {
        if (string.IsNullOrWhiteSpace(content)) return Result.Failure<Content>(EmptyContentFailure.Instance);
        content = content.Trim();

        if (content.Length > MaxLenght)
            return Result.Failure<Content>(new ContentMaxLengthExceededFailure(MaxLenght, content.Length));

        if (content.Length < MinLength)
            return Result.Failure<Content>(new ContentTooShortFailure(MinLength, content.Length));

        foreach (var blackListedWord in _blacklistedWorlds)
        {
            content = content.Replace(blackListedWord, "***", StringComparison.OrdinalIgnoreCase);
        }

        return Result.Success(new Content(content));
    }
}