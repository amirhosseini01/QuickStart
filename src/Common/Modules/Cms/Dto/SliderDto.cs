using System.ComponentModel.DataAnnotations;
using Common.Commons;
using Microsoft.AspNetCore.Http;

namespace Common.Modules.Cms;

public class SliderDto
{

}

public class SliderListDto
{
    public int Id { get; set; }
    public int? ViewOrder { get; set; }
    public string Title { get; set; }
    public SliderPlace SliderPlace { get; set; }
    public bool Visible { get; set; } = true;
    public string Image { get; set; }
    public string Thumbnail { get; set; }
    public string? Link { get; set; }
}

public class SliderListFilterDto : PaginatedListFilter
{
    public bool? Visible { get; internal set; }
}

public class SliderDetailDto
{
    public int Id { get; set; }
    public int? ViewOrder { get; set; }
    public string Title { get; set; }
    public SliderPlace SliderPlace { get; set; }
    public bool Visible { get; set; } = true;
    public string Image { get; set; }
    public string Thumbnail { get; set; }
    public string? Link { get; set; }
}

public class SliderAdminInputDto
{
    public int? ViewOrder { get; set; }

    [Required]
    [StringLength(maximumLength: ModelStatics.TitleRequiredLength, MinimumLength = ModelStatics.TitleMinimumLength)]
    public string Title { get; set; }

    [Required]
    public SliderPlace SliderPlace { get; set; }

    public bool Visible { get; set; } = true;

    [Required]
    public IFormFile Image { get; set; }

    [Required]
    public IFormFile Thumbnail { get; set; }

    [StringLength(maximumLength: ModelStatics.UrlRequiredLength, MinimumLength = ModelStatics.UrlMinimumLength)]
    public string? Link { get; set; }
}

public class SliderAdminInputEditDto : SliderAdminInputDto
{
    public new IFormFile? Image { get; set; }
    public new IFormFile? Thumbnail { get; set; }
}