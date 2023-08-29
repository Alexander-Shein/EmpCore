namespace CommentManagementService.WebApi.Comments.Models;

public class CreateCommentInputModel
{
    public Guid PublishedBlogPostId { get; set; }
    public string Message { get; set; }
}
