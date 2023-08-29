using BlogPostManagementService.Application.BlogPosts.Queries.GetBlogPostById.DTOs;
using Dapper;
using EmpCore.QueryStack.Dapper;
using MediatR;
using System.Data;

namespace BlogPostManagementService.Application.BlogPosts.Queries.GetBlogPostById
{
    public class GetBlogPostByIdQueryHandler : IRequestHandler<GetBlogPostByIdQuery, BlogPostDto>
    {
        private readonly IConnectionFactory _connectionFactory;

        public GetBlogPostByIdQueryHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<BlogPostDto> Handle(GetBlogPostByIdQuery query, CancellationToken ct)
        {
            const string SQL = @"
SELECT TOP (1) [Id]
      ,[AuthorId]
      ,[Title]
      ,[Content]
      ,[FeedbackEmailAddress]
      ,[PublishStatus]
      ,[PublishDateTime]
      ,[IsDeleted]
      ,[CreatedAt]
      ,[UpdatedAt]
  FROM [dbo].[BlogPost]
  WHERE [Id] = @BlogPostId AND [IsDeleted] = 0;

  SELECT [Id]
      ,[BlogPostId]
      ,[Url]
      ,[Caption]
  FROM [dbo].[EmbeddedResource]
  WHERE [BlogPostId] = @BlogPostId
  ORDER BY [Id] ASC;";

            if (query == null) throw new ArgumentNullException(nameof(query));

            var prms = new DynamicParameters();
            prms.Add(nameof(query.BlogPostId), query.BlogPostId, DbType.Guid);

            using (var dbConn = await _connectionFactory.CreateConnectionAsync().ConfigureAwait(false))
            using (var dbQuery = await dbConn.QueryMultipleAsync(SQL, prms).ConfigureAwait(false))
            {
                var dto = await dbQuery.ReadFirstOrDefaultAsync<BlogPostDto>().ConfigureAwait(false);
                if (dto == null) return dto;

                dto.EmbeddedResourses = (await dbQuery.ReadAsync<ContentEmbeddedResourceDto>().ConfigureAwait(false)).ToList();
                return dto;
            }
        }
    }
}