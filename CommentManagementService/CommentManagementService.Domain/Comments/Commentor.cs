using CommentManagementService.Domain.Comments.ValueObjects;
using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments;

public class Commentor : Entity<Guid>
{
    public UserName UserName { get; private set; }

    public static Result<Commentor> Create(Guid id, UserName userName)
    {
        if (id == Guid.Empty) throw new ArgumentException("Empty commentor id.", nameof(id));

        var commentor = new Commentor
        {
            Id = id,
            UserName = userName ?? throw new ArgumentNullException(nameof(userName))
        };

        return Result.Success(commentor);
    }
}