using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Api.Common;

namespace Api.Modules.Product;

public class ProductStock : BaseEntityCreate
{
    [Required]
    [StringLength(ModelStatics.UserRequiredLength)]
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public int ProductModelId { get; set; }
    public int Value { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public Product Product { get; set; }
    
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public ProductModel ProductModel { get; set; }
    
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public AppUser User { get; set; }
}