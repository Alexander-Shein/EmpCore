using BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost.DTOs;
using EmpCore.Application.Commands;
using EmpCore.Domain;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.UpdateBlogPost;

public class UpdateBlogPostCommand : Command, IRequest<Result>
{
    public string UpdatedBy { get; }
    public Guid BlogPostId { get; }
    public string? Title { get; }
    public string? Content { get; }
    public IReadOnlyList<EmbeddedResourceDto>? EmbeddedResources { get; }

    public UpdateBlogPostCommand(
        string updatedBy,
        Guid blogPostId,
        string? title,
        string? content,
        IReadOnlyList<EmbeddedResourceDto>? embeddedResources)
    {
        UpdatedBy = updatedBy;
        BlogPostId = blogPostId;
        Title = title;
        Content = content;
        EmbeddedResources = embeddedResources;
    }
}
