using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Product;

public class ProductBrand: BaseEntity
{
    [Required]
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }
    public bool Visible { get; set; }

    public ICollection<Product> Products { get; }
}