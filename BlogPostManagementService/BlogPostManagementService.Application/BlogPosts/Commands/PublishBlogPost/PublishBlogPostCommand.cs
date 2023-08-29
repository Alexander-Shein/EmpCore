using EmpCore.Application.Commands;
using EmpCore.Domain;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.PublishBlogPost;

public class PublishBlogPostCommand : Command, IRequest<Result>
{
    public string PublishedBy { get; }
    public Guid BlogPostId { get; }

    public PublishBlogPostCommand(string publishedBy, Guid blogPostId)
    {
        PublishedBy = publishedBy;
        BlogPostId = blogPostId;
    }
}
