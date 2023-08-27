using EmpCore.Api.Middleware.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostManagementService.WebApi.BlogPosts.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogPostsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPrincipalUser _principalUser;

    public BlogPostsController(IMediator mediator, IPrincipalUser principalUser)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _principalUser = principalUser ?? throw new ArgumentNullException(nameof(principalUser));
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
    }
}