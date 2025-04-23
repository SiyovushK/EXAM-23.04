namespace Domain.DTOs.AnalyticsDTOs;

public class AdjestmentDTO
{
    public DateTime AdjustmentDate { get; set; }
    public int Amount { get; set; }
    public string Reason { get; set; }
}