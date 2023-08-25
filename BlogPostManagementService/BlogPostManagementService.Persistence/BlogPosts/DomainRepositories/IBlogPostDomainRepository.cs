using BlogPostManagementService.Domain.BlogPosts;
using EmpCore.Infrastructure.Persistence;

namespace BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;

public interface IBlogPostDomainRepository : IDomainRepository<BlogPost, Guid>
{
    public void Save(BlogPost blogPost);
}