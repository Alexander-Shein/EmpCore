using EmpCore.Application.Commands;
using EmpCore.Domain;
using MediatR;

namespace CommentManagementService.Application.Comments.Commands.AddComment;

public class AddCommentCommand : Command, IRequest<Result>
{
    public Guid BlogPostId { get; }
    public Guid CommentorId { get; }
    public string CommentorUserName { get; }
    public string Message { get; }
    
    public AddCommentCommand(
        Guid blogPostId,
        Guid commentorId,
        string commentorUserName,
        string message)
    {
        BlogPostId = blogPostId;
        CommentorId = commentorId;
        CommentorUserName = commentorUserName;
        Message = message;
    }
}