using EmpCore.Application.Commands;
using EmpCore.Domain;
using MediatR;

namespace CommentManagementService.Application.Comments.Commands.AddComment;

public class AddCommentCommand : Command, IRequest<Result<long>>
{
    public Guid PublishedBlogPostId { get; }
    public Guid CommentorId { get; }
    public string CommentorUserName { get; }
    public string Message { get; }
    
    public AddCommentCommand(
        Guid publishedBlogPostId,
        Guid commentorId,
        string commentorUserName,
        string message)
    {
        PublishedBlogPostId = publishedBlogPostId;
        CommentorId = commentorId;
        CommentorUserName = commentorUserName;
        Message = message;
    }
}