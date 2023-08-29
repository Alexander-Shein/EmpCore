using CommentManagementService.Domain.Comments;
using CommentManagementService.Domain.Comments.ValueObjects;
using CommentManagementService.Persistence.BlogPosts.DomainRepositories;
using CommentManagementService.Persistence.Comments.DomainRepositories;
using EmpCore.Application.ApplicationFailures;
using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;
using MediatR;

namespace CommentManagementService.Application.Comments.Commands.AddComment;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Result<long>>
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

    public async Task<Result<long>> Handle(AddCommentCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var commentor = BuildCommentor(command.CommentorId, command.CommentorUserName);
        var message = Message.Create(command.Message);

        var result = Result.Combine(commentor, message);
        if (result.IsFailure) return Result.Failure<long>(result.Failures);

        var blogPost = await _publishedBlogPostDomainRepository.GetByIdAsync(command.PublishedBlogPostId).ConfigureAwait(false);
        if (blogPost == null) return Result.Failure<long>(ResourceNotFoundFailure.Instance);

        var comment = blogPost.Comment(commentor, message);
        if (comment.IsFailure) return Result.Failure<long>(comment.Failures);

        _commentDomainRepository.Save(comment);
        await _unitOfWork.SaveAsync().ConfigureAwait(false);
        
        return Result.Success(comment.Value.Id);
    }

    private static Result<Commentor> BuildCommentor(string commentorId, string userName)
    {
        var userNameResult = UserName.Create(userName);
        if (userNameResult.IsFailure) return Result.Failure<Commentor>(userNameResult.Failures);
        
        var commentor = Commentor.Create(commentorId, userNameResult);
        return commentor;
    }
}