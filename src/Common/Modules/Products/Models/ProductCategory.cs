using System.ComponentModel.DataAnnotations;
using Common.Commons;
using Common.Data;

namespace Common.Modules.Product;

public class ProductCategory : BaseEntity
{
    public int? ViewOrder { get; set; }
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }
    public bool Visible { get; set; }

    [StringLength(ModelStatics.ImageRequiredLength)]
    public string? Image { get; set; }

    public ICollection<Product> Products { get; }
}