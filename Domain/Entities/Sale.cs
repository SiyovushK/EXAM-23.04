namespace Domain.Entities;

public class Sale
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int QuantitySold { get; set; }
    public DateTime SaleDate { get; set; }

    public virtual Product Product { get; set; }
}