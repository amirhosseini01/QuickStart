using System.ComponentModel.DataAnnotations;
using Api.Common;

namespace Api.Modules.Product;

public class ProductBrandVm
{

}

public class ProductBrandListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Visible { get; set; }
}

public class ProductBrandListFilterDto : PaginatedListFilter
{

}

public class ProductBrandDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Visible { get; set; }

}

public class ProductBrandAdminInputDto
{
    [Required]
    [StringLength(maximumLength: ModelStatics.TitleRequiredLength, MinimumLength = ModelStatics.TitleMinimumLength)]
    public string Title { get; set; }

    public bool Visible { get; set; } = true;
}