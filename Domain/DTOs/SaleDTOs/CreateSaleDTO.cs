namespace Domain.DTOs.SaleDTOs;

public class CreateSaleDTO
{
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
    public DateTime SaleDate { get; set; }
}