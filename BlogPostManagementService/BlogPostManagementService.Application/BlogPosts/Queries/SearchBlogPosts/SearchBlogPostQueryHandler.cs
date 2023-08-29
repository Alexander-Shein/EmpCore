using BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts.DTOs;
using Dapper;
using EmpCore.Application.Queries;
using EmpCore.QueryStack.Dapper;
using MediatR;

namespace BlogPostManagementService.Application.BlogPosts.Queries.SearchBlogPosts
{
    public class SearchBlogPostQueryHandler : IRequestHandler<SearchBlogPostsQuery, PagedList<BlogPostListItemDto>>
    {
        private readonly IConnectionFactory _connectionFactory;

        public SearchBlogPostQueryHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<PagedList<BlogPostListItemDto>> Handle(SearchBlogPostsQuery query, CancellationToken ct)
        {
            const string SQL = @"
SELECT TOP (1000) [Id]
      ,[AuthorId]
      ,[FeedbackEmailAddress]
      ,[Title]
      ,[PublishDateTime]
      ,[CreatedAt]
  FROM [dbo].[BlogPost]
  WHERE [IsDeleted] = 0
ORDER BY [PublishDateTime] DESC;";

            if (query == null) throw new ArgumentNullException(nameof(query));

            var prms = new DynamicParameters();

            using (var dbConn = await _connectionFactory.CreateConnectionAsync().ConfigureAwait(false))
            {
                var dtos = (await dbConn.QueryAsync<BlogPostListItemDto>(SQL, prms).ConfigureAwait(false)).ToList();
                return new PagedList<BlogPostListItemDto>(dtos.Count, 100, 1, "CreatedAt", SortDir.Desc, dtos);
            }
        }
    }
}