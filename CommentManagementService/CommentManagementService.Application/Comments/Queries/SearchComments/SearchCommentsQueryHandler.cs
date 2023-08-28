using CommentManagementService.Application.Comments.Queries.SearchComments.DTOs;
using Dapper;
using EmpCore.Application.Queries;
using EmpCore.QueryStack.Dapper;
using MediatR;

namespace CommentManagementService.Application.Comments.Queries.SearchComments;

public class SearchCommentsQueryHandler : IRequestHandler<SearchCommentsQuery, PagedList<CommentListItemDto>>
{
    private readonly IConnectionFactory _connectionFactory;

    public SearchCommentsQueryHandler(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public async Task<PagedList<CommentListItemDto>> Handle(SearchCommentsQuery query, CancellationToken ct)
    {
        const string SQL = @"
SELECT TOP (1000) [Id]
      ,[PublishedBlogPostId]
      ,[CommentorId]
      ,[Message]
      ,[CreatedAt]
      ,[UpdatedAt]
  FROM [dbo].[Comment]
ORDER BY [CreatedAt] DESC;";

        if (query == null) throw new ArgumentNullException(nameof(query));

        var prms = new DynamicParameters();

        using (var dbConn = await _connectionFactory.CreateConnectionAsync().ConfigureAwait(false))
        {
            var dtos = (await dbConn.QueryAsync<CommentListItemDto>(SQL, prms).ConfigureAwait(false)).ToList();
            return new PagedList<CommentListItemDto>(dtos.Count, 100, 1, "CreatedAt", SortDir.Desc, dtos);
        }
    }
}