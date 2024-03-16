using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Common.Commons;
using Common.Data;
using Common.Modules.User;

namespace Common.Modules.Product;

public class ProductComment : BaseEntity
{
    [StringLength(ModelStatics.UserRequiredLength)]
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public bool Approved { get; set; }

    [StringLength(ModelStatics.DescriptionRequiredLength)]
    public string Comment { get; set; }
    public DateTime CreateDate { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Product Product { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public AppUser User { get; set; }
}