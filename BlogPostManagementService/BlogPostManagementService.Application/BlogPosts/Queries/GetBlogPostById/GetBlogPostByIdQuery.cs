using BlogPostManagementService.Application.BlogPosts.Queries.GetBlogPostById.DTOs;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Queries.GetBlogPostById
{
    public class GetBlogPostByIdQuery : IRequest<BlogPostDto>
    {
        public Guid BlogPostId { get; }

        public GetBlogPostByIdQuery(Guid blogPostId)
        {
            BlogPostId = blogPostId;
        }
    }
}