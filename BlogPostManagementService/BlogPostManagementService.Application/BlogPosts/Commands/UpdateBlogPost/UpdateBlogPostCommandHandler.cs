using BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost.DTOs;
using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;
using EmpCore.Application.ApplicationFailures;
using EmpCore.Application.Middleware.DomainEventsDispatcher;
using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.UpdateBlogPost;

public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, Result>
{
    private readonly IDomainEventsHolder _domainEventsHolder;
    private readonly IBlogPostDomainRepository _blogPostDomainRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBlogPostCommandHandler(
        IDomainEventsHolder domainEventsHolder,
        IBlogPostDomainRepository blogPostDomainRepository,
        IUnitOfWork unitOfWork)
    {
        _domainEventsHolder = domainEventsHolder ?? throw new ArgumentNullException(nameof(domainEventsHolder));
        _blogPostDomainRepository = blogPostDomainRepository ?? throw new ArgumentNullException(nameof(blogPostDomainRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Handle(UpdateBlogPostCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var blogPost = await _blogPostDomainRepository.GetByIdAsync(command.BlogPostId).ConfigureAwait(false); ;
        if (blogPost == null) return Result.Failure(ResourceNotFoundFailure.Instance);

        Title? newTitle = null;
        if (command.Title != null)
        {
            var title = Title.Create(command.Title);
            if (title.IsFailure) return title;
            newTitle = title;
        }

        Content? newContent = null;
        if (command.Content != null || command.EmbeddedResources != null)
        {
            var text = command.Content ?? blogPost.Content.Text;
            Result<Content> content;

            if (command.EmbeddedResources == null)
            {
                content = Content.Create(text, blogPost.Content.EmbeddedResources);
            }
            else
            {
                content = BuildContent(text, command.EmbeddedResources);
            }
            if (content.IsFailure) return content;
            newContent = content;
        }

        var result = blogPost.Update(command.UpdatedBy, newTitle, newContent);
        if (result.IsFailure) return result;

        _blogPostDomainRepository.Update(blogPost);
        await _unitOfWork.SaveAsync().ConfigureAwait(false);

        _domainEventsHolder.AddFrom(blogPost);

        return Result.Success();
    }

    private static Result<Content> BuildContent(string text, IEnumerable<EmbeddedResourceDto> embeddedResources)
    {
        var embeddedResourcesDomain = Enumerable.Empty<EmbeddedResource>();
        
        if (embeddedResources.Any())
        {
            var embeddedResourcesResult = embeddedResources
                .Select(er => EmbeddedResource.Create(new Uri(er.Url), er.Caption))
                .ToArray();

            var result = Result.Combine(embeddedResourcesResult);
            if (result.IsFailure) return Result.Failure<Content>(result.Failures);
            embeddedResourcesDomain = embeddedResourcesResult.Select(er => er.Value);
        }
        
        var content = Content.Create(text, embeddedResourcesDomain);
        return content;
    }
}