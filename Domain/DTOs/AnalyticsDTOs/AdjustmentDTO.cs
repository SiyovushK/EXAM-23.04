using Domain.DTOs.SaleDTOs;
using Domain.DTOs.StockAdjectmentDTOs;

namespace Domain.DTOs.AnalyticsDTOs;

public class AdjustmentDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal QuantityInStock { get; set; }
    public string Category { get; set; }
    public string Supplier { get; set; }

    public List<QuantityAndDateOfSale> Sales { get; set; }
    public List<AdjestmentDTO> Adjustments { get; set; }
}