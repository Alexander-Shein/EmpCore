using BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts.DTOs;
using EmpCore.Application.Queries;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts
{
    public class SearchBlogPostsQuery : IRequest<PagedList<BlogPostListItemDto>>
    {
    }
}