using CommentManagementService.Domain.Comments;
using CommentManagementService.Domain.Comments.ValueObjects;
using CommentManagementService.Persistence.Comments.DomainRepositories;
using EmpCore.Application.ApplicationFailures;
using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;
using MediatR;

namespace CommentManagementService.Application.Comments.Commands.ReplyToComment;

public class ReplyToCommentCommandHandler : IRequestHandler<ReplyToCommentCommand, Result<long>>
{
    private readonly ICommentDomainRepository _commentDomainRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReplyToCommentCommandHandler(
        ICommentDomainRepository commentDomainRepository,
        IUnitOfWork unitOfWork)
    {
        _commentDomainRepository = commentDomainRepository ?? throw new ArgumentNullException(nameof(commentDomainRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<long>> Handle(ReplyToCommentCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var commentor = BuildCommentor(command.CommentorId, command.CommentorUserName);
        var message = Message.Create(command.Message);

        var result = Result.Combine(commentor, message);
        if (result.IsFailure) return Result.Failure<long>(result.Failures);

        var comment = await _commentDomainRepository.GetByIdAsync(command.CommentId).ConfigureAwait(false);
        if (comment == null) return Result.Failure<long>(ResourceNotFoundFailure.Instance);

        var reply = comment.Reply(commentor, message);
        if (reply.IsFailure) return Result.Failure<long>(reply.Failures);

        _commentDomainRepository.Save(reply);
        await _unitOfWork.SaveAsync().ConfigureAwait(false);
        
        return Result.Success(reply.Value.Id);
    }

    private static Result<Commentor> BuildCommentor(string commentorId, string userName)
    {
        var userNameResult = UserName.Create(userName);
        if (userNameResult.IsFailure) return Result.Failure<Commentor>(userNameResult.Failures);
        
        var commentor = Commentor.Create(commentorId, userNameResult);
        return commentor;
    }
}