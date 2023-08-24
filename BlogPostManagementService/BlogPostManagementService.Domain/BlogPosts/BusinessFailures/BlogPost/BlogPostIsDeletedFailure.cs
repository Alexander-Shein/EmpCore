using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts.BusinessFailures.BlogPost;

public class BlogPostIsDeletedFailure : Failure
{
    private const string ErrorCode = "blog_post_is_deleted";
    private const string ErrorMessage = "The blog post has been deleted. Please create a new one.";

    public Guid BlogPostId { get; }

    public BlogPostIsDeletedFailure(Guid blogPostId) : base(ErrorCode, ErrorMessage)
    {
        BlogPostId = blogPostId;
    }
}