using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Product;

public class ProductPropertyValue : BaseEntity
{
    public int ProductId { get; set; }
    public int ProductPropertyId { get; set; }
    
    [Required]
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Value { get; set; }

    public Product Product { get; set; }
    public ProductProperty ProductProperty { get; set; }
}