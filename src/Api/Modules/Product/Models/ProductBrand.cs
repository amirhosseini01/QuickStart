using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Product;

public class ProductBrand: BaseEntity
{
    [Required]
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }
    public bool Visible { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public ICollection<Product> Products { get; }
}