using CommentManagementService.Application.Comments.Commands.AddComment;
using CommentManagementService.Application.Comments.Commands.ReplyToComment;
using CommentManagementService.Application.Comments.Queries.SearchComments;
using CommentManagementService.Application.Comments.Queries.SearchComments.DTOs;
using CommentManagementService.WebApi.Comments.Models;
using EmpCore.Api.Middleware.Security;
using EmpCore.Application.Queries;
using EmpCore.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CommentManagementService.WebApi.Comments.Controllers;

[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
[Authorize]
[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPrincipalUser _principalUser;

    public CommentsController(IMediator mediator, IPrincipalUser principalUser)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _principalUser = principalUser ?? throw new ArgumentNullException(nameof(principalUser));
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PagedList<CommentListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedList<CommentListItemDto>>> SearchCommentsAsync([FromQuery] SearchCommentsInputModel im)
    {
        var query = new SearchCommentsQuery(im.BlogPostId, im.PageSize, im.PageNumber, im.SortField, im.SortDir);

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(CreateCommentViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Failure>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentInputModel im)
    {
        var command = new AddCommentCommand(im.PublishedBlogPostId,
            _principalUser.Id, _principalUser.PreferredUserName, im.Message);

        var result = await _mediator.Send(command).ConfigureAwait(false);
        if (result.IsFailure) return UnprocessableEntity(result.Failures);
        return Ok(new CreateCommentViewModel { Id = result });
    }

    [HttpPost("{commentId}/replies")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(CreateCommentViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Failure>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> ReplyToCommentAsync(long commentId, [FromBody] ReplyToCommentInputModel im)
    {
        var command = new ReplyToCommentCommand(commentId,
            _principalUser.Id, _principalUser.PreferredUserName, im.Message);

        var result = await _mediator.Send(command).ConfigureAwait(false);
        if (result.IsFailure) return UnprocessableEntity(result.Failures);
        return Ok(new CreateCommentViewModel { Id = result });
    }
}