using System.ComponentModel.DataAnnotations;
using Common.Commons;
using Microsoft.AspNetCore.Http;

namespace Common.Modules.Product;

public class ProductCategoryVm
{

}

public class ProductCategoryAdminListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Visible { get; set; }
    public int? ViewOrder { get; set; }
    public string? Image { get; set; }
}

public class ProductCategoryListFilterDto : PaginatedListFilter
{
    public bool? Visible { get; set; }
}

public class ProductCategoryAdminDetailDto
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

public class ProductCategoryListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? ViewOrder { get; set; }
    public string? Image { get; set; }
}