namespace Domain.Response;

public class PagedResposne<T>(T data, int pageSize, int pageNumber, int totalRecords) : Response<T>(data)
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public int TotalPages { get; set; } = (int)Math.Ceiling(totalRecords / (double)pageSize);
    public int TotalRecords { get; set; } = totalRecords;
}