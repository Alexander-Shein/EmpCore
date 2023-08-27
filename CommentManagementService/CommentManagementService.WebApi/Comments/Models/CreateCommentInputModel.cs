namespace CommentManagementService.WebApi.Comments.Models;

public class CreateCommentInputModel
{
    public Guid PublishedBlogPostId { get; private set; }
    public string Message { get; private set; }
}
