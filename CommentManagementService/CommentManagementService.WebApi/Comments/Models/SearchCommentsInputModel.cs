using EmpCore.Application.Queries;

namespace CommentManagementService.WebApi.Comments.Models;

public class SearchCommentsInputModel
{
    public Guid? BlogPostId { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string SortField { get; set; }
    public SortDir SortDir { get; set; }
}
