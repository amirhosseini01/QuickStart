using System.ComponentModel.DataAnnotations;
using Api.Common;

namespace Api.Modules.Product;

public class ProductBrand: BaseEntity
{
    public int? ViewOrder { get; set; }
    
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }
    public bool Visible { get; set; }

    [StringLength(ModelStatics.ImageRequiredLength)]
    public string? Logo { get; set; }

    public ICollection<Product> Products { get; }
}