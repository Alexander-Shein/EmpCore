using BlogPostManagementService.Contracts;
using CommentManagementService.Domain.BlogPosts;
using CommentManagementService.Persistence.BlogPosts.DomainRepositories;
using DotNetCore.CAP;
using EmpCore.Infrastructure.Persistence;

namespace CommentManagementService.Application.BlogPosts.Events;

public class BlogPostPublishedEventHandler : ICapSubscribe
{
    private readonly IPublishedBlogPostDomainRepository _publishedBlogPostDomainRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public BlogPostPublishedEventHandler(
        IPublishedBlogPostDomainRepository publishedBlogPostDomainRepository,
        IUnitOfWork unitOfWork)
    {
        _publishedBlogPostDomainRepository = publishedBlogPostDomainRepository
                                             ?? throw new ArgumentNullException(nameof(publishedBlogPostDomainRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    [CapSubscribe(BlogPostPublishedEvent.EventName)]
    public async Task HandleAsync(BlogPostPublishedEvent @event)
    {
        if (@event == null) throw new ArgumentNullException(nameof(@event));

        var blogPost = await _publishedBlogPostDomainRepository.GetByIdAsync(@event.BlogPostId).ConfigureAwait(false);
        if (blogPost == null) return;

        blogPost = PublishedBlogPost.Create(@event.BlogPostId);
        
        _publishedBlogPostDomainRepository.Save(blogPost);

        await _unitOfWork.SaveAsync().ConfigureAwait(false);
    }
}