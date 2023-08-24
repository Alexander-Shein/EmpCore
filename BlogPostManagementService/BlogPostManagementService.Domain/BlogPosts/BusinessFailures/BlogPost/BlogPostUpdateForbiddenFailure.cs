using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.BlogPost;

public class BlogPostUpdateForbiddenFailure : Failure
{
    private const string ErrorCode = "blog_post_update_forbidden";
    private const string ErrorMessage = "Owner only is allowed to modify blog posts.";

    public Guid BlogPostId { get; }

    public BlogPostUpdateForbiddenFailure(Guid blogPostId) : base(ErrorCode, ErrorMessage)
    {
        BlogPostId = blogPostId;
    }
}