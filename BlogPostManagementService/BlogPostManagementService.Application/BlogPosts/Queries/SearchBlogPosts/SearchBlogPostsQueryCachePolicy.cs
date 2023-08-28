using BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts.DTOs;
using EmpCore.Application.Middleware.Caching;
using EmpCore.Application.Queries;

namespace BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts
{
    public class SearchBlogPostsQueryCachePolicy : CachePolicy<SearchBlogPostsQuery, PagedList<BlogPostListItemDto>>
    {
        public override TimeSpan? AbsoluteExpirationRelativeToNow => TimeSpan.FromSeconds(15);
    }
}