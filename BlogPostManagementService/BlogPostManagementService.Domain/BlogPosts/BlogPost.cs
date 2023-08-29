using BlogPostManagementService.Domain.BlogPosts.BusinessFailures.BlogPost;
using BlogPostManagementService.Domain.BlogPosts.DomainEvents;
using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts;

public class BlogPost : AggregateRoot<Guid>
{
    public Author Author { get; private set; }

    public Title Title { get; private set; }
    public Content Content { get; private set; }

    public PublishStatus PublishStatus { get; private set; }
    public DateTime? PublishDateTime { get; private set; }

    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public static Result<BlogPost> CreateDraftBlogPost(
        Author author,
        Title title,
        Content content)
    {
        var now = DateTime.UtcNow;

        var blogPost = new BlogPost
        {
            Id = Guid.NewGuid(),
            Author = author ?? throw new ArgumentNullException(nameof(author)),
            Title = title ?? throw new ArgumentNullException(nameof(title)),
            PublishStatus = PublishStatus.Draft,
            PublishDateTime = null,
            Content = content ?? throw new ArgumentNullException(nameof(content)),
            IsDeleted = false,
            CreatedAt = now,
            UpdatedAt = now
        };

        return Result.Success(blogPost);
    }

    public Result Update(string updatedBy, Title? title = null, Content? content = null)
    {
        if (Author.Id != updatedBy) return Result.Failure(new BlogPostUpdateForbiddenFailure(Id));
        if (IsDeleted) return Result.Failure(new BlogPostIsDeletedFailure(Id));
        
        var now = DateTime.UtcNow;
        
        if (title != null && title != Title)
        {
            Title = title;
            UpdatedAt = now;
        }

        if (content != null && content != Content)
        {
            Content = content;
            UpdatedAt = now;
        }

        return Result.Success();
    }

    public Result Publish(string publishedBy)
    {
        if (Author.Id != publishedBy) return Result.Failure(new BlogPostUpdateForbiddenFailure(Id));
        if (IsDeleted) return Result.Failure(new BlogPostIsDeletedFailure(Id));

        PublishStatus = PublishStatus.Released;
        PublishDateTime = UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new BlogPostPublishedDomainEvent(Id, Author.Id, PublishDateTime.Value, Author.FeedbackEmailAddress));
        return Result.Success();
    }
    
    public Result Delete(string deletedBy)
    {
        if (Author.Id != deletedBy) return Result.Failure(new BlogPostUpdateForbiddenFailure(Id));
        if (IsDeleted) return Result.Success();

        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new BlogPostDeletedDomainEvent(Id, Author.Id));
        return Result.Success();
    }
}