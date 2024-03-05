using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Api.Common;
using Api.Modules.Users;

namespace Api.Modules.Product;

public class ProductComment: BaseEntity
{
    [StringLength(ModelStatics.UserRequiredLength)]
    public string UserId { get; set; }
    public int ProductId { get; set; }

    [StringLength(ModelStatics.DescriptionRequiredLength)]
    public string Comment { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Product Product { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public AppUser User { get; set; }
}