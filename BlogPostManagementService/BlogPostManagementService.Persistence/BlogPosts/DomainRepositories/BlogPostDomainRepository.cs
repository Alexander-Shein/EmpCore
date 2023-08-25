using BlogPostManagementService.Domain.BlogPosts;
using EmpCore.Persistence.EntityFrameworkCore;

namespace BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;

public class BlogPostDomainRepository : IBlogPostDomainRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BlogPostDomainRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public void Save(BlogPost blogPost)
    {
        _dbContext.Add(blogPost);
    }
}