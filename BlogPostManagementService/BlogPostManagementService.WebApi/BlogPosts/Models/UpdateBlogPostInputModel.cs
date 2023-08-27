using BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost.DTOs;

namespace BlogPostManagementService.WebApi.BlogPosts.Models;

public class UpdateBlogPostInputModel
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public IReadOnlyList<EmbeddedResourceDto>? EmbeddedResources { get; set; }
}
