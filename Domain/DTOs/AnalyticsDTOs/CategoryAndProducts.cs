namespace Domain.DTOs.AnalyticsDTOs;

public class CategoryAndProducts
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<ProductsParameters> Products { get; set; }
}