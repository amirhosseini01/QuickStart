using System.ComponentModel.DataAnnotations;
using Common.Commons;
using Microsoft.AspNetCore.Http;

namespace Common.Modules.Product;

public class ProductVm
{

}

public class ProductAdminListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public bool Saleable { get; set; }
    public bool Visible { get; set; }
    public bool IsSpecialOffer { get; set; }
}

public class ProductListFilterDto : PaginatedListFilter
{
    public bool? Visible { get; set; }
    public bool? Saleable { get; set; }
    public bool? IsSpecialOffer { get; set; }
}

public class ProductDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public bool Saleable { get; set; }
    public bool Visible { get; set; }
    public bool IsSpecialOffer { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public ProductProductSellerDto ProductSeller { get; set; }
    public ProductProductBrandDto ProductBrand { get; set; }
    public ProductProductCategoryDto ProductCategory { get; set; }
    public ICollection<ProductProductModelDto> ProductModels { get; init; }

}
public class ProductProductSellerDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Logo { get; set; }
}
public class ProductProductCategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}
public class ProductProductBrandDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}

public class ProductProductModelDto
{
    public int? ViewOrder { get; set; }
    public string Title { get; set; }
    public ProductModelType Type { get; set; }
    public string Value { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}

public class ProductAdminInputDto
{
    [Required]
    [Range(minimum: ModelStatics.MinimumIdRange, maximum: ModelStatics.MaximumIdRange)]
    public int ProductCategoryId { get; set; }

    [Required]
    [Range(minimum: ModelStatics.MinimumIdRange, maximum: ModelStatics.MaximumIdRange)]
    public int ProductBrandId { get; set; }

    [Required]
    [Range(minimum: ModelStatics.MinimumIdRange, maximum: ModelStatics.MaximumIdRange)]
    public int ProductSellerId { get; set; }

    [Range(minimum: ModelStatics.MinimumIdRange, maximum: ModelStatics.MaximumIdRange)]
    public int? ViewOrder { get; set; }

    [Required]
    [StringLength(maximumLength: ModelStatics.TitleRequiredLength, MinimumLength = ModelStatics.TitleMinimumLength)]
    public string Title { get; set; }

    [Required]
    public IFormFile Image { get; set; }

    [Required]
    public IFormFile Thumbnail { get; set; }

    public bool Visible { get; set; } = true;
    public bool Saleable { get; set; } = true;
    public bool IsSpecialOffer { get; set; }

    [Required]
    [StringLength(maximumLength: ModelStatics.DescriptionRequiredLength, MinimumLength = ModelStatics.DescriptionMinimumLength)]
    public string ShortDescription { get; set; }

    [Required]
    [MinLength(ModelStatics.DescriptionMinimumLength)]
    public string Description { get; set; }

}

public class ProductAdminInputEditDto : ProductAdminInputDto
{
    public new IFormFile? Image { get; set; }
    public new IFormFile? Thumbnail { get; set; }
}

public class ProductListDto
{
    public int Id { get; set; }
    public string CategoryTitle { get; set; }
    public int? ViewOrder { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public decimal? Price { get; set; }
    public decimal? Discount { get; set; }
}

public class ProductTopSaleListDto : ProductListDto
{
    public int SaleCount { get; set; }
}
public class ProductToViewListDto : ProductListDto
{
    public int ViewCount { get; set; }
}