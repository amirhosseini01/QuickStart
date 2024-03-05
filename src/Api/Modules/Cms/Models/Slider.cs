using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Common;

namespace Api.Modules.Cms;
public class Slider: BaseEntity
{
    public int? ViewOrder { get; set; }
 
    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }

    [Column(TypeName = ModelStatics.Nvarchar50)]
    public SliderPlace SliderPlace { get; set; }
    
    public bool Visible { get; set; } = true;
 
    [StringLength(ModelStatics.ImageRequiredLength)]
    public string Image { get; set; }
    
    [StringLength(ModelStatics.ImageRequiredLength)]
    public string Thumbnail { get; set; }

    [StringLength(ModelStatics.UrlRequiredLength)]
    public string? Link { get; set; }
}