using System.ComponentModel.DataAnnotations;
using Api.Common;

namespace Api.Modules.Product;

public class ProductModelDto
{

}

public class ProductModelListDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int? ViewOrder { get; set; }
    public string Title { get; set; }
    public ProductModelType Type { get; set; }
    public string Value { get; set; }
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
}

public class ProductModelDetailDto
{
    public int ProductId { get; set; }
    public int? ViewOrder { get; set; }
    public string Title { get; set; }
    public ProductModelType Type { get; set; }
    public string Value { get; set; }
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
}

public class ProductModelListFilterDto: PaginatedListFilter
{

}
public class ProductModelAdminInputDto
{
    [Required]
    [Range(minimum: ModelStatics.MinimumIdRange, maximum: ModelStatics.MaximumIdRange)]
    public int ProductId { get; set; }

    public int? ViewOrder { get; set; }

    [Required]
    [StringLength(maximumLength: ModelStatics.TitleRequiredLength, MinimumLength = ModelStatics.TitleMinimumLength)]
    public string Title { get; set; }

    [Required]
    public ProductModelType Type { get; set; }

    [Required]
    [StringLength(maximumLength: ModelStatics.TitleRequiredLength, MinimumLength = ModelStatics.TitleMinimumLength)]
    public string Value { get; set; }
    
    [Required]
    public decimal Price { get; set; }

    public decimal? Discount { get; set; }
}