namespace Domain.Filters;

public class ProductFilter
{
    public string? Name { get; set; } = string.Empty;
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}