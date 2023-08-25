using BlogPostManagementService.Domain.BlogPosts;
using EmpCore.Infrastructure.Persistence;

namespace BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;

public interface IBlogPostDomainRepository : IDomainRepository<BlogPost, Guid>
{
    Task<BlogPost?> GetByIdAsync(Guid blogPostId);
    public void Save(BlogPost blogPost);
    void Update(BlogPost blogPost);
}