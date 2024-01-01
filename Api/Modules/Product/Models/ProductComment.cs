using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Product;

public class ProductComment: BaseEntity
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(ModelStatics.DescriptionRequiredLength)]
    public string Comment { get; set; }

    public Product Product { get; set; }
}