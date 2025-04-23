namespace Domain.DTOs.SaleDTOs;

public class GetSaleDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
    public DateTime SaleDate { get; set; }
}