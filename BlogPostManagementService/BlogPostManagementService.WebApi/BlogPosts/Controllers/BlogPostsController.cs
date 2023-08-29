using System.Net.Mime;
using BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost;
using BlogPostManagementService.Application.BlogPosts.Commands.DeleteBlogPost;
using BlogPostManagementService.Application.BlogPosts.Commands.PublishBlogPost;
using BlogPostManagementService.Application.BlogPosts.Commands.UpdateBlogPost;
using BlogPostManagementService.Application.BlogPosts.Queries.GetBlogPostById;
using BlogPostManagementService.Application.BlogPosts.Queries.GetBlogPostById.DTOs;
using BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts;
using BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts.DTOs;
using BlogPostManagementService.WebApi.BlogPosts.Models;
using EmpCore.Api.Middleware.Security;
using EmpCore.Application.Queries;
using EmpCore.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostManagementService.WebApi.BlogPosts.Controllers;

[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
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
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PagedList<BlogPostListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedList<BlogPostListItemDto>>> SearchBlogPostsAsync([FromQuery] SearchBlogPostsInputModel im)
    {
        var query = new SearchBlogPostsQuery(im.PageSize, im.PageNumber, im.SortField, im.SortDir);
        
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{blogPostId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(BlogPostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlogPostDto>> GetBlogPostByIdAsync(Guid blogPostId)
    {
        var query = new GetBlogPostByIdQuery(blogPostId);
        
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(CreateBlogPostViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Failure>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateBlogPostAsync([FromBody] CreateBlogPostInputModel im)
    {
        var command = new CreateDraftBlogPostCommand(
            _principalUser.Id, _principalUser.Email, im.Title, im.Content, im.EmbeddedResources);

        var result = await _mediator.Send(command).ConfigureAwait(false);
        if (result.IsFailure) return UnprocessableEntity(result.Failures);
        return Ok(new CreateBlogPostViewModel { Id = result });
    }

    [HttpPatch("{blogPostId}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<Failure>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateBlogPostAsync(Guid blogPostId, [FromBody] UpdateBlogPostInputModel im)
    {
        var command = new UpdateBlogPostCommand(
            _principalUser.Id, blogPostId, im.Title, im.Content, im.EmbeddedResources);

        var result = await _mediator.Send(command).ConfigureAwait(false);
        if (result.IsFailure) return UnprocessableEntity(result.Failures);
        return NoContent();
    }

    [HttpDelete("{blogPostId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(List<Failure>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> DeleteBlogPostAsync(Guid blogPostId)
    {
        var command = new DeleteBlogPostCommand(_principalUser.Id, blogPostId);

        var result = await _mediator.Send(command).ConfigureAwait(false);
        if (result.IsFailure) return UnprocessableEntity(result.Failures);
        return NoContent();
    }

    [HttpPut("/v{version}/published-blog-posts/{blogPostId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<Failure>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> PublishBlogPostAsync(Guid blogPostId)
    {
        var command = new PublishBlogPostCommand(_principalUser.Id, blogPostId);

        var result = await _mediator.Send(command).ConfigureAwait(false);
        if (result.IsFailure) return UnprocessableEntity(result.Failures);
        return NoContent();
    }
}