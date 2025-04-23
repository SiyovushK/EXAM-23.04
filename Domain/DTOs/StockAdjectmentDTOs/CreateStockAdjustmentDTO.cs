namespace Domain.DTOs.StockAdjectmentDTOs;

public class CreateStockAdjustmentDTO
{
    public int ProductId { get; set; }
    public int AdjustmentAmount { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime AdjustmentDate { get; set; }
}