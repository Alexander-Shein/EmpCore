using CommentManagementService.Application.Comments.Queries.SearchComments.DTOs;
using EmpCore.Application.Middleware.Caching;
using EmpCore.Application.Queries;

namespace CommentManagementService.Application.Comments.Queries.SearchComments;

public class SearchCommentsQueryCachePolicy : CachePolicy<SearchCommentsQuery, PagedList<CommentListItemDto>>
{
    public override TimeSpan? AbsoluteExpirationRelativeToNow => TimeSpan.FromSeconds(15);
}