using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.EmbeddedResourse;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.ValueObjects;

public class EmbeddedResource : ValueObject
{
    public Uri Url { get; }
    public string Caption { get; }

    private EmbeddedResource(Uri url, string caption)
    {
        Url = url;
        Caption = caption;
    }

    public static Result<EmbeddedResource> Create(Uri url, string caption)
    {
        if (url == null) return Result.Failure<EmbeddedResource>(EmptyUrlFailure.Instance);
        if (String.IsNullOrEmpty(caption)) return Result.Failure<EmbeddedResource>(EmptyCaptionFailure.Instance);

        return Result.Success(new EmbeddedResource(url, caption.Trim()));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Url.OriginalString;
        yield return Caption;
    }
}