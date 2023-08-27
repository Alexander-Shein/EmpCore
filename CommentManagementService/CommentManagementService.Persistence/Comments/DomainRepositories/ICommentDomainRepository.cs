using CommentManagementService.Domain.BlogPosts;
using CommentManagementService.Domain.Comments;
using EmpCore.Infrastructure.Persistence;

namespace CommentManagementService.Persistence.Comments.DomainRepositories;

public interface ICommentDomainRepository : IDomainRepository<PublishedBlogPost, Guid>
{
    public Task<Comment?> GetByIdAsync(long commentId);
    public void Save(Comment comment);
}