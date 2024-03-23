using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Common.Commons;
using Common.Modules.User;
using Common.Data;

namespace Common.Modules.Product;

public class ProductStock : BaseEntity
{
    [StringLength(ModelStatics.UserRequiredLength)]
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public int ProductModelId { get; set; }
    public bool IsReserved { get; set; }
    public int Value { get; set; }
    public DateTime CreateDate { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Product Product { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public ProductModel ProductModel { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public AppUser User { get; set; }
}