using EmpCore.Application.Commands;
using EmpCore.Domain;
using MediatR;

namespace CommentManagementService.Application.Comments.Commands.ReplyToComment;

public class ReplyToCommentCommand : Command, IRequest<Result<long>>
{
    public long CommentId { get; }
    public string CommentorId { get; }
    public string CommentorUserName { get; }
    public string Message { get; }
    
    public ReplyToCommentCommand(
        long commentId,
        string commentorId,
        string commentorUserName,
        string message)
    {
        CommentId = commentId;
        CommentorId = commentorId;
        CommentorUserName = commentorUserName;
        Message = message;
    }
}