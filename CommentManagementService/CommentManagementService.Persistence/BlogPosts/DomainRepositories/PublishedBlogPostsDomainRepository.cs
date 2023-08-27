using CommentManagementService.Domain.BlogPosts;
using EmpCore.Persistence.EntityFrameworkCore;

namespace CommentManagementService.Persistence.BlogPosts.DomainRepositories;

public class PublishedBlogPostsDomainRepository : IPublishedBlogPostDomainRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PublishedBlogPostsDomainRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<PublishedBlogPost?> GetByIdAsync(Guid blogPostId)
    {
        if (blogPostId == Guid.Empty) return null;
        
        return await _dbContext.FindAsync<PublishedBlogPost>(blogPostId).ConfigureAwait(false);
    }

    public void Save(PublishedBlogPost blogPost)
    {
        _dbContext.Add(blogPost ?? throw new ArgumentNullException(nameof(blogPost)));
    }

    public void Delete(PublishedBlogPost blogPost)
    {
        _dbContext.Remove(blogPost ?? throw new ArgumentNullException(nameof(blogPost)));
    }
}