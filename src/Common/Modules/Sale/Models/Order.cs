using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Commons;
using Common.Data;
using Common.Modules.Product;
using Common.Modules.User;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Sale;

public class Order : BaseEntity
{
    [StringLength(ModelStatics.UserRequiredLength)]
    public string UserId { get; set; }
    public int? UserAddressId { get; set; }

    [Column(TypeName = ModelStatics.Nvarchar50)]
    public OrderStatus OrderStatus { get; set; }

    [StringLength(ModelStatics.DescriptionRequiredLength)]
    public string? AdminNote { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public AppUser User { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public UserAddress UserAddress { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; }
}