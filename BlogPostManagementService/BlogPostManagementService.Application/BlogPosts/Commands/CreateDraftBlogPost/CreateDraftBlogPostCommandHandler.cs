using BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost.DTOs;
using BlogPostManagementService.Domain.BlogPosts;
using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;
using EmpCore.Application.Middleware.DomainEventsDispatcher;
using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost;

public class CreateDraftBlogPostCommandHandler : IRequestHandler<CreateDraftBlogPostCommand, Result<Guid>>
{
    private readonly IDomainEventsHolder _domainEventsHolder;
    private readonly IBlogPostDomainRepository _blogPostDomainRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDraftBlogPostCommandHandler(
        IDomainEventsHolder domainEventsHolder,
        IBlogPostDomainRepository blogPostDomainRepository,
        IUnitOfWork unitOfWork)
    {
        _domainEventsHolder = domainEventsHolder ?? throw new ArgumentNullException(nameof(domainEventsHolder));
        _blogPostDomainRepository = blogPostDomainRepository ?? throw new ArgumentNullException(nameof(blogPostDomainRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Handle(CreateDraftBlogPostCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var author = BuildAuthor(command.AuthorId, command.FeedbackEmailAddress);
        var title = Title.Create(command.Title);
        var content = BuildContent(command.Content, command.EmbeddedResources);
        
        var result = Result.Combine(author, title, content);
        if (result.IsFailure) return Result.Failure<Guid>(result.Failures);

        var blogPost = BlogPost.CreateDraftBlogPost(author, title, content);
        if (blogPost.IsFailure) return Result.Failure<Guid>(result.Failures);
        
        _blogPostDomainRepository.Save(blogPost);
        await _unitOfWork.SaveAsync().ConfigureAwait(false);

        _domainEventsHolder.AddFrom(blogPost.Value);
        
        return Result.Success(blogPost.Value.Id);
    }

    private static Result<Author> BuildAuthor(string authorId, string feedbackEmailAddress)
    {
        var feedbackEmailAddressResult = EmailAddress.Create(feedbackEmailAddress);
        if (feedbackEmailAddressResult.IsFailure) return Result.Failure<Author>(feedbackEmailAddressResult.Failures);
        
        var author = Author.Create(authorId, feedbackEmailAddressResult);
        return author;
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