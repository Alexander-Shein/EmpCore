using BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;
using EmpCore.Application.ApplicationFailures;
using EmpCore.Application.Middleware.DomainEventsDispatcher;
using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.PublishBlogPost;

public class PublishBlogPostCommandHandler : IRequestHandler<PublishBlogPostCommand, Result>
{
    private readonly IDomainEventsHolder _domainEventsHolder;
    private readonly IBlogPostDomainRepository _blogPostDomainRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PublishBlogPostCommandHandler(
        IDomainEventsHolder domainEventsHolder,
        IBlogPostDomainRepository blogPostDomainRepository,
        IUnitOfWork unitOfWork)
    {
        _domainEventsHolder = domainEventsHolder ?? throw new ArgumentNullException(nameof(domainEventsHolder));
        _blogPostDomainRepository = blogPostDomainRepository ?? throw new ArgumentNullException(nameof(blogPostDomainRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Handle(PublishBlogPostCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var blogPost = await _blogPostDomainRepository.GetByIdAsync(command.BlogPostId).ConfigureAwait(false); ;
        if (blogPost == null) return Result.Failure(ResourceNotFoundFailure.Instance);

        var result = blogPost.Publish(command.PublishedBy);
        if (result.IsFailure) return result;

        _blogPostDomainRepository.Update(blogPost);
        await _unitOfWork.SaveAsync().ConfigureAwait(false);

        _domainEventsHolder.AddFrom(blogPost);

        return Result.Success();
    }
}