using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Product.Cms;
public class Slider: BaseEntity
{
    [Required]
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }
    
    [Required]
    [StringLength(ModelStatics.ImageRequiredLength)]
    public string Image { get; set; }
    
    [Required]
    [StringLength(ModelStatics.ImageRequiredLength)]
    public string Thumbnail { get; set; }

    [Required]
    [StringLength(ModelStatics.UrlRequiredLength)]
    public string Link { get; set; }
}