using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts;

public class BlogPost : AggregateRoot<Guid>
{
    public Guid AuthorId { get; }
    public Title Title { get; }
    public PublishStatus PublishStatus { get; }
    public DateTime PublishDateTime { get; }
    public Content Content { get; }
    public IReadOnlyList<EmbeddedResource> EmbeddedResources => _embeddedResourses.ToList();
    private List<EmbeddedResource> _embeddedResourses = new();
    public EmailAddress FeedbackEmailAddress { get; }
    public bool IsDeleted { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

    public static Result<BlogPost> CreateDraftBlogPost()
    {

    }
}