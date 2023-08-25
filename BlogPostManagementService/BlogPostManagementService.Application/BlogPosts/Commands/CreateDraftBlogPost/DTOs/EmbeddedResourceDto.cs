namespace BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost.DTOs;

public class EmbeddedResourceDto
{
    public string Url { get; }
    public string Caption { get; }

    public EmbeddedResourceDto(string url, string caption)
    {
        Url = url;
        Caption = caption;
    }
}