using CommentManagementService.Domain.BlogPosts.BusinessFailures;
using CommentManagementService.Domain.Comments;
using CommentManagementService.Domain.Comments.ValueObjects;
using EmpCore.Domain;

namespace CommentManagementService.Domain.BlogPosts;

public class PublishedBlogPost : AggregateRoot<Guid>
{
    public static Result<PublishedBlogPost> Create(Guid id)
    {
        if (id == Guid.Empty) return Result.Failure<PublishedBlogPost>(EmptyBlogPostIdFailure.Instance);
        return Result.Success(new PublishedBlogPost{Id = id});
    }
    
    public Result<Comment> Comment(Commentor commentor, Message message)
    {
        var comment = new Comment(this,
            commentor ?? throw new ArgumentNullException(nameof(commentor)),
            message ?? throw new ArgumentNullException(nameof(message)),
            null);

        return Result.Success(comment);
    }
}