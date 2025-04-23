using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int QuantityInStock { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }

    public virtual Category Category { get; set; }
    public virtual Supplier Supplier { get; set; }
    public virtual List<Sale> Sales { get; set; }
    public virtual List<StockAdjustment> StockAdjustments { get; set; }
}