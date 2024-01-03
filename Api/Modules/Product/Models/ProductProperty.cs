using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Product;

public class ProductProperty : BaseEntity
{
    public int ProductCategoryId { get; set; }

    [Required]
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }

    public ProductCategory ProductCategory { get; set; }
    public ICollection<ProductPropertyValue> ProductPropertyValues { get; }
}