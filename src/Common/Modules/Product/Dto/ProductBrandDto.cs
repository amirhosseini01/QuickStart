using System.ComponentModel.DataAnnotations;
using Common.Commons;
using Microsoft.AspNetCore.Http;

namespace Common.Modules.Product;

public class ProductBrandVm
{

}

public class ProductBrandListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Visible { get; set; }
    public int? ViewOrder { get; set; }
    public string? Logo { get; set; }
}

public class ProductBrandListFilterDto : PaginatedListFilter
{
    public bool? Visible { get; set; }
}

public class ProductBrandDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Visible { get; set; }
    public int? ViewOrder { get; set; }
    public string? Logo { get; set; }

}

public class ProductBrandAdminInputDto
{
    [Required]
    [StringLength(maximumLength: ModelStatics.TitleRequiredLength, MinimumLength = ModelStatics.TitleMinimumLength)]
    public string Title { get; set; }

    public bool Visible { get; set; } = true;
    public int? ViewOrder { get; set; }
    public IFormFile? Logo { get; set; }
}