using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.Content;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class Content : ValueObject
{
    private const int MinLength = 100;
    private const int MaxLenght = 100_000;
    private static readonly IEnumerable<string> BlacklistedWorlds = new List<string> { "Word1", "Word2", "Word3" };

    public string Text { get; }
    public IReadOnlyList<EmbeddedResource> EmbeddedResources => _embeddedResources.ToList();
    private List<EmbeddedResource> _embeddedResources;

    private Content(string text) : this(text, Enumerable.Empty<EmbeddedResource>())
    {
    }
    
    private Content(string text, IEnumerable<EmbeddedResource> embeddedResources)
    {
        Text = text;
        _embeddedResources = embeddedResources.ToList();
    }

    public static Result<Content> Create(string text, IEnumerable<EmbeddedResource> embeddedResources)
    {
        if (string.IsNullOrWhiteSpace(text)) return Result.Failure<Content>(EmptyContentFailure.Instance);
        text = text.Trim();

        if (text.Length > MaxLenght)
            return Result.Failure<Content>(new ContentMaxLengthExceededFailure(MaxLenght, text.Length));

        if (text.Length < MinLength)
            return Result.Failure<Content>(new ContentTooShortFailure(MinLength, text.Length));

        foreach (var blackListedWord in BlacklistedWorlds)
        {
            text = text.Replace(blackListedWord, "***", StringComparison.OrdinalIgnoreCase);
        }

        return Result.Success(new Content(text, embeddedResources));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Text;
        foreach (var embeddedResource in EmbeddedResources)
        {
            yield return embeddedResource;
        }
    }
}