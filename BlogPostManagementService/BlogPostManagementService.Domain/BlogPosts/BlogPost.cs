using BlogPostManagementService.Domain.BlockPosts.ValueObjects;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlockPosts;

public class BlogPost : AggregateRoot<Guid>
{
    public Guid AuthorId { get; }
    public Title Title { get; }
    public PublishStatus PublishStatus { get; }
    public DateTime PublishDateTime { get; }
    public Content Content { get; }
    public IReadOnlyList<EmbeddedResourse> EmbeddedResourses => _embeddedResourses.ToList();
    private List<EmbeddedResourse> _embeddedResourses = new List<EmbeddedResourse>();
    public EmailAddress FeedbackEmailAddress { get; }
    public bool IsDeleted { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

    public static Result<BlogPost> CreateDraftBlogPost()
    {

    }
}