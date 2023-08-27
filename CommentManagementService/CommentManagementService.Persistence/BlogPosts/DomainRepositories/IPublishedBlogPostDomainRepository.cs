using CommentManagementService.Domain.BlogPosts;
using EmpCore.Infrastructure.Persistence;

namespace CommentManagementService.Persistence.BlogPosts.DomainRepositories;

public interface IPublishedBlogPostDomainRepository : IDomainRepository<PublishedBlogPost, Guid>
{
    public Task<PublishedBlogPost?> GetByIdAsync(Guid blogPostId);
    public void Save(PublishedBlogPost blogPost);
    public void Delete(PublishedBlogPost blogPost);
}