using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Common.Commons;
using Common.Data;

namespace Common.Modules.Product;

public class Product : BaseEntity
{
    public int ProductCategoryId { get; set; }
    public int ProductBrandId { get; set; }
    public int ProductSellerId { get; set; }

    public int? ViewOrder { get; set; }

    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }

    [StringLength(ModelStatics.ImageRequiredLength)]
    public string Image { get; set; }

    [StringLength(ModelStatics.ImageRequiredLength)]
    public string Thumbnail { get; set; }

    public bool Visible { get; set; } = true;
    public bool Saleable { get; set; } = true;
    public bool IsSpecialOffer { get; set; }

    [StringLength(ModelStatics.DescriptionRequiredLength)]
    public string ShortDescription { get; set; }

    public string Description { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public ProductSeller ProductSeller { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public ProductCategory ProductCategory { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public ProductBrand ProductBrand { get; set; }
    public ICollection<ProductComment> ProductComments { get; }
    public ICollection<ProductModel> ProductModels { get; }
    public ICollection<ProductStock> ProductStocks { get; }
}