using BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts.DTOs;
using EmpCore.Application.Queries;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts
{
    public class SearchBlogPostsQuery : IRequest<PagedList<BlogPostListItemDto>>
    {
        public int PageSize { get; }
        public int PageNumber { get; }
        public string SortField { get; }
        public SortDir SortDir { get; }
        
        public SearchBlogPostsQuery(int pageSize, int pageNumber, string sortField, SortDir sortDir)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            SortField = sortField;
            SortDir = sortDir;
        }
    }
}