using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Product;

public class ProductGallery: BaseEntity
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(ModelStatics.PhoneNumberRequiredLength)]
    public string Alt { get; set; }

    [Required]
    [StringLength(ModelStatics.ImageRequiredLength)]
    public string Image { get; set; }

    public Product Product { get; set; }
}