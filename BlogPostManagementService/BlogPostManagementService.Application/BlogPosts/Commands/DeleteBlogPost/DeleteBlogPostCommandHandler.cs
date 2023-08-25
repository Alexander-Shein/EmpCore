using BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;
using EmpCore.Application.Middleware.DomainEventsDispatcher;
using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.DeleteBlogPost;

public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, Result>
{
    private readonly IDomainEventsHolder _domainEventsHolder;
    private readonly IBlogPostDomainRepository _blogPostDomainRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBlogPostCommandHandler(
        IDomainEventsHolder domainEventsHolder,
        IBlogPostDomainRepository blogPostDomainRepository,
        IUnitOfWork unitOfWork)
    {
        _domainEventsHolder = domainEventsHolder ?? throw new ArgumentNullException(nameof(domainEventsHolder));
        _blogPostDomainRepository = blogPostDomainRepository ?? throw new ArgumentNullException(nameof(blogPostDomainRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Handle(DeleteBlogPostCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var blogPost = await _blogPostDomainRepository.GetByIdAsync(command.BlogPostId).ConfigureAwait(false); ;
        if (blogPost == null) return Result.Success();

        var result = blogPost.Delete(command.DeletedBy);
        if (result.IsFailure) return result;

        _blogPostDomainRepository.Update(blogPost);
        await _unitOfWork.SaveAsync().ConfigureAwait(false);

        _domainEventsHolder.AddFrom(blogPost);

        return Result.Success();
    }
}