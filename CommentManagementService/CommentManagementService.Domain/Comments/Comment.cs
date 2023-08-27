using CommentManagementService.Domain.BlogPosts;
using CommentManagementService.Domain.Comments.ValueObjects;
using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments;

public class Comment : AggregateRoot<long>
{
    private Comment() { }

    public PublishedBlogPost PublishedBlogPost { get; private set; }
    public Commentor Commentor { get; private set; }
    public Comment? ParentComment { get; private set; }
    public Message Message { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    internal Comment(PublishedBlogPost publishedBlogPost, Commentor commentor, Message message, Comment? parentComment)
    {
        var now = DateTime.UtcNow;

        PublishedBlogPost = publishedBlogPost ?? throw new ArgumentNullException(nameof(publishedBlogPost));
        Commentor = commentor ?? throw new ArgumentNullException(nameof(commentor));
        Message = message ?? throw new ArgumentNullException(nameof(message));
        ParentComment = parentComment;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public Result<Comment> Reply(Commentor commentor, Message message)
    {
        var comment = new Comment(PublishedBlogPost,
            commentor ?? throw new ArgumentNullException(nameof(commentor)),
            message ?? throw new ArgumentNullException(nameof(message)),
            this);

        return Result.Success(comment);
    }
}