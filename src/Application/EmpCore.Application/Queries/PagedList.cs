namespace EmpCore.Application.Queries
{
    public class PagedList<T>
    {
        public int Total { get; }
        public int PageSize { get; }
        public int PageNumber { get; }
        public string SortField { get; }
        public SortDir SortDir { get; }
        public IReadOnlyList<T> Data { get; }

        public PagedList(int total, int pageSize, int pageNumber, string sortField, SortDir sortDir, IEnumerable<T> data)
        {
            Total = total;
            PageSize = pageSize;
            PageNumber = pageNumber;
            SortField = sortField;
            SortDir = sortDir;
            Data = data.ToList();
        }
    }
}
