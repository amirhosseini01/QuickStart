using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Product;

public class ProductComment: BaseEntity
{
    public string UserId { get; set; }
    public int ProductId { get; set; }

    [Required]
    [StringLength(ModelStatics.DescriptionRequiredLength)]
    public string Comment { get; set; }

    public Product Product { get; set; }
    public AppUser User { get; set; }
}