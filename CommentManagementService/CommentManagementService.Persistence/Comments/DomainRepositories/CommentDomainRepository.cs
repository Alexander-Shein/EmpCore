using CommentManagementService.Domain.Comments;
using EmpCore.Persistence.EntityFrameworkCore;

namespace CommentManagementService.Persistence.Comments.DomainRepositories;

public class CommentDomainRepository : ICommentDomainRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommentDomainRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Comment?> GetByIdAsync(long commentId)
    {
        if (commentId <= 0) return null;

        return await _dbContext.FindAsync<Comment>(commentId).ConfigureAwait(false);
    }

    public void Save(Comment comment)
    {
        _dbContext.Add(comment ?? throw new ArgumentNullException(nameof(comment)));
        _dbContext.Attach(comment.Commentor);
    }
}