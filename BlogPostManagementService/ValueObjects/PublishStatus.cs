using BlogPostManagementService.Domain.BlockPosts.BusinessFailures;
using EmpCore.Domain;
using System.Collections.Immutable;

namespace BlogPostManagementService.Domain.BlockPosts.ValueObjects;

public class PublishStatus : SingleValueObject<string>
{
    public static readonly PublishStatus Draft = new PublishStatus(nameof(Draft));
    public static readonly PublishStatus Released = new PublishStatus(nameof(Released));

    public static readonly IReadOnlyList<PublishStatus> List = new[] { Draft, Released }.ToImmutableList();

    private PublishStatus(string value) : base(value) { }

    public static Result<PublishStatus> From(string publishStatus)
    {
        if (String.IsNullOrWhiteSpace(publishStatus))
            return Result.Failure<PublishStatus>(new InvalidPublishStatusFailure(publishStatus));

        publishStatus = publishStatus.Trim();

        var state = List.FirstOrDefault(s =>
            String.Equals(s.Value, publishStatus, StringComparison.OrdinalIgnoreCase));

        if (state == null) return Result.Failure<PublishStatus>(new InvalidPublishStatusFailure(publishStatus));
        return Result.Success(state);
    }
}
