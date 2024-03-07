using System.ComponentModel.DataAnnotations;
using Api.Common;

namespace Api.Modules.Product;

public class ProductCategoryVm
{

}

public class ProductCategoryListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Visible { get; set; }
    public int? ViewOrder { get; set; }
    public string? Image { get; set; }
}

public class ProductCategoryListFilterDto : PaginatedListFilter
{

}

public class ProductCategoryDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Visible { get; set; }
    public int? ViewOrder { get; set; }
    public string? Image { get; set; }

}

public class ProductCategoryAdminInputDto
{
    [Required]
    [StringLength(maximumLength: ModelStatics.TitleRequiredLength, MinimumLength = ModelStatics.TitleMinimumLength)]
    public string Title { get; set; }

    public bool Visible { get; set; } = true;
    public int? ViewOrder { get; set; }

    public IFormFile? Image { get; set; }
}