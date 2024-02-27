using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Api.Common;

namespace Api.Modules.Product;

public class ProductModel : BaseEntity
{
    public int ProductId { get; set; }

    public int? ViewOrder { get; set; }

    [Required]
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }

    [Column(TypeName = ModelStatics.Nvarchar50)]
    public ProductModelType Type { get; set; }
    
    [Required]
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Value { get; set; }
    public int Price { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Product Product { get; set; }
    public ICollection<ProductStock> ProductStocks { get; }
}