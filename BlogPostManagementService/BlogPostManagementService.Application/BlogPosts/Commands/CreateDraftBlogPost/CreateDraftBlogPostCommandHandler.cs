using EmpCore.Application.Middleware.DomainEventsDispatcher;
using EmpCore.Domain;
using EmpCore.Infrastructure.Persistence;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost;

public class CreateDraftBlogPostCommandHandler : IRequestHandler<CreateDraftBlogPostCommand, Result<Guid>>
{
    private readonly IDomainEventsHolder _domainEventsHolder;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDraftBlogPostCommandHandler(IDomainEventsHolder domainEventsHolder, IUnitOfWork unitOfWork)
    {
        _domainEventsHolder = domainEventsHolder ?? throw new ArgumentNullException(nameof(domainEventsHolder));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Handle(CreateDraftBlogPostCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        await _unitOfWork.SaveAsync().ConfigureAwait(false);
    }
}