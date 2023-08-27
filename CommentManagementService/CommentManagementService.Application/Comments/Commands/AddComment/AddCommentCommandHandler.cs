using CommentManagementService.Domain.Comments;
using CommentManagementService.Domain.Comments.ValueObjects;
using CommentManagementService.Persistence.BlogPosts.DomainRepositories;
using CommentManagementService.Persistence.Comments.DomainRepositories;
using EmpCore.Application.ApplicationFailures;
using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;
using MediatR;

namespace CommentManagementService.Application.Comments.Commands.AddComment;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Result>
{
    private readonly IPublishedBlogPostDomainRepository _publishedBlogPostDomainRepository;
    private readonly ICommentDomainRepository _commentDomainRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCommentCommandHandler(
        IPublishedBlogPostDomainRepository publishedBlogPostDomainRepository,
        ICommentDomainRepository commentDomainRepository,
        IUnitOfWork unitOfWork)
    {
        _commentDomainRepository = commentDomainRepository ?? throw new ArgumentNullException(nameof(commentDomainRepository));
        _publishedBlogPostDomainRepository = publishedBlogPostDomainRepository ?? throw new ArgumentNullException(nameof(publishedBlogPostDomainRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Handle(AddCommentCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var commentor = BuildCommentor(command.CommentorId, command.CommentorUserName);
        var message = Message.Create(command.Message);

        var result = Result.Combine(commentor, message);
        if (result.IsFailure) return result;

        var blogPost = await _publishedBlogPostDomainRepository.GetByIdAsync(command.BlogPostId).ConfigureAwait(false);
        if (blogPost == null) return Result.Failure(ResourceNotFoundFailure.Instance);

        var comment = blogPost.Comment(commentor, message);
        if (comment.IsFailure) return comment;

        _commentDomainRepository.Save(comment);
        await _unitOfWork.SaveAsync().ConfigureAwait(false);
        
        return Result.Success();
    }

    private static Result<Commentor> BuildCommentor(Guid commentorId, string userName)
    {
        var userNameResult = UserName.Create(userName);
        if (userNameResult.IsFailure) return Result.Failure<Commentor>(userNameResult.Failures);
        
        var commentor = Commentor.Create(commentorId, userNameResult);
        return commentor;
    }
}