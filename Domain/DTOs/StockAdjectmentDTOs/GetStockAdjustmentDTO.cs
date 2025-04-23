namespace Domain.DTOs.StockAdjectmentDTOs;

public class GetStockAdjustmentDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AdjustmentAmount { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime AdjustmentDate { get; set; }
}