using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Common.Commons;
using Common.Data;
using Common.Modules.Sale;

namespace Common.Modules.Product;

public class ProductModel : BaseEntity
{
    public int ProductId { get; set; }

    public int? ViewOrder { get; set; }

    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }

    [Column(TypeName = ModelStatics.Nvarchar50)]
    public ProductModelType Type { get; set; }

    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Value { get; set; }
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Product Product { get; set; }
    public ICollection<ProductStock> ProductStocks { get; }
    public ICollection<OrderDetail> OrderDetails { get; }
}