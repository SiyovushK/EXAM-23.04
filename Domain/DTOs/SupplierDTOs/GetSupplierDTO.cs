namespace Domain.DTOs.SupplierDTOs;

public class GetSupplierDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}