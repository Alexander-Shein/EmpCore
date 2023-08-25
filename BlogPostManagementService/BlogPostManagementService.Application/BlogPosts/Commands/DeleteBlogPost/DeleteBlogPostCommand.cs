using EmpCore.Application.Commands;
using EmpCore.Domain;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.DeleteBlogPost;

public class DeleteBlogPostCommand : Command, IRequest<Result>
{
    public Guid DeletedBy { get; }
    public Guid BlogPostId { get; }

    public DeleteBlogPostCommand(Guid deletedBy, Guid blogPostId)
    {
        DeletedBy = deletedBy;
        BlogPostId = blogPostId;
    }
}
