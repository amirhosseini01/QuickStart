using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Product;

public class ProductCategory: BaseEntity
{
    [Required]
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }
    public bool Visible { get; set; }

    public ICollection<Product> Products { get; }
    public ICollection<ProductProperty> ProductProperties { get; }
}