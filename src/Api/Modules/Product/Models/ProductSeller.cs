using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Product;

public class ProductSeller : BaseEntity
{
    public string UserId { get; set; }

    [Required]
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }

    [Required]
    [StringLength(ModelStatics.PhoneNumberRequiredLength)]
    public string PhoneNumber { get; set; }

    [StringLength(ModelStatics.ImageRequiredLength)]
    public string Logo { get; set; }

    [StringLength(ModelStatics.DescriptionRequiredLength)]
    public string PostalAddress { get; set; }
    public AppUser User { get; set; }
}