using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public virtual List<Product> Products { get; set; }
}