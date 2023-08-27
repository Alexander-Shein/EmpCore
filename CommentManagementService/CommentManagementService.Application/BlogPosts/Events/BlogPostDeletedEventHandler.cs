using BlogPostManagementService.Contracts;
using CommentManagementService.Persistence.BlogPosts.DomainRepositories;
using DotNetCore.CAP;
using EmpCore.Infrastructure.Persistence;

namespace CommentManagementService.Application.BlogPosts.Events;

public class BlogPostDeletedEventHandler : ICapSubscribe
{
    private readonly IPublishedBlogPostDomainRepository _publishedBlogPostDomainRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public BlogPostDeletedEventHandler(
        IPublishedBlogPostDomainRepository publishedBlogPostDomainRepository,
        IUnitOfWork unitOfWork)
    {
        _publishedBlogPostDomainRepository = publishedBlogPostDomainRepository
                                             ?? throw new ArgumentNullException(nameof(publishedBlogPostDomainRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    [CapSubscribe(BlogPostDeletedEvent.EventName)]
    public async Task HandleAsync(BlogPostDeletedEvent @event)
    {
        if (@event == null) throw new ArgumentNullException(nameof(@event));

        var blogPost = await _publishedBlogPostDomainRepository.GetByIdAsync(@event.BlogPostId).ConfigureAwait(false);
        if (blogPost == null) return;
        
        _publishedBlogPostDomainRepository.Delete(blogPost);

        await _unitOfWork.SaveAsync().ConfigureAwait(false);
    }
}