using Domain.DTOs.ProductDTOs;

namespace Domain.DTOs.AnalyticsDTOs;

public class SupplierDTO
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public List<string> Products { get; set; }
}