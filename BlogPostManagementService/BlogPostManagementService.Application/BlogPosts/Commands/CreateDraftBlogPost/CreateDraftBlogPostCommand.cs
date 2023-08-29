using BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost.DTOs;
using EmpCore.Application.Commands;
using EmpCore.Domain;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost;

public class CreateDraftBlogPostCommand : Command, IRequest<Result<Guid>>
{
    public string AuthorId { get; }
    public string FeedbackEmailAddress { get; }
    public string Title { get; }
    public string Content { get; }
    public IReadOnlyList<EmbeddedResourceDto> EmbeddedResources { get; }

    public CreateDraftBlogPostCommand(
        string authorId,
        string feedbackEmailAddress,
        string title,
        string content,
        IEnumerable<EmbeddedResourceDto> embeddedResources)
    {
        AuthorId = authorId;
        FeedbackEmailAddress = feedbackEmailAddress;
        Title = title;
        Content = content;
        EmbeddedResources = embeddedResources?.ToList() ?? new List<EmbeddedResourceDto>();
    }
}