namespace Domain.Filters;

public class SaleFilter
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}