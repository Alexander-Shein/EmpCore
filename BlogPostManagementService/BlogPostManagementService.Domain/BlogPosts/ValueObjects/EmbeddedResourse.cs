using BlogPostManagementService.Domain.BlockPosts.BusinessFailures.EmbeddedResourse;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlockPosts.ValueObjects;

public class EmbeddedResourse : ValueObject
{
    public Uri Url { get; }
    public string Caption { get; }

    private EmbeddedResourse(Uri url, string caption)
    {
        Url = url;
        Caption = caption;
    }

    public static Result<EmbeddedResourse> Create(Uri url, string caption)
    {
        if (url == null) return Result.Failure<EmbeddedResourse>(EmptyUrlFailure.Instance);
        if (String.IsNullOrEmpty(caption)) return Result.Failure<EmbeddedResourse>(EmptyCaptionFailure.Instance);

        return Result.Success(new EmbeddedResourse(url, caption.Trim()));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Url.OriginalString;
        yield return Caption;
    }
}