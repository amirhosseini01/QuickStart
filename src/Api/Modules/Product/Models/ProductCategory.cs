using System.ComponentModel.DataAnnotations;
using Api.Common;

namespace Api.Modules.Product;

public class ProductCategory: BaseEntity
{
    [Required]
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }
    public bool Visible { get; set; }

    public ICollection<Product> Products { get; }
}