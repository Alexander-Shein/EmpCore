using EmpCore.Domain;

namespace CommentManagementService.Domain.BlogPosts.BusinessFailures;

public class EmptyBlogPostIdFailure : Failure
{
    private const string ErrorCode = "empty_blog_post_id";
    private static readonly string ErrorMessage = "Blog post id must not be empty.";

    public static readonly EmptyBlogPostIdFailure Instance = new();

    private EmptyBlogPostIdFailure() : base(ErrorCode, ErrorMessage) { }
}