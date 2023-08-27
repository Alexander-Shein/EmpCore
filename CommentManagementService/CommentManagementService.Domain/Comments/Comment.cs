using CommentManagementService.Domain.BlogPosts;
using CommentManagementService.Domain.Comments.ValueObjects;
using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments;

public class Comment : AggregateRoot<long>
{
    public PublishedBlogPost BlogPost { get; private set; }
    public Commentor Commentor { get; private set; }
    public Comment? ParentComment { get; private set; }
    public Message Message { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    internal Comment(PublishedBlogPost blogPost, Commentor commentor, Message message, Comment? parentComment)
    {
        var now = DateTime.UtcNow;
        
        BlogPost = blogPost ?? throw new ArgumentNullException(nameof(blogPost));
        Commentor = commentor ?? throw new ArgumentNullException(nameof(commentor));
        Message = message ?? throw new ArgumentNullException(nameof(message));
        ParentComment = parentComment;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public Result<Comment> Reply(Commentor commentor, Message message)
    {
        var comment = new Comment(BlogPost,
            commentor ?? throw new ArgumentNullException(nameof(commentor)),
            message ?? throw new ArgumentNullException(nameof(message)),
            this);

        return Result.Success(comment);
    }
}