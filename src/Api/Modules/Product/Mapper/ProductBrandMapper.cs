using Riok.Mapperly.Abstractions;

namespace Api.Modules.Product;

[Mapper]
public static partial class ProductBrandMapperQuery
{
    public static partial IQueryable<ProductBrandDetailDto> MapProductBrandToDetailDto(this IQueryable<ProductBrand> q);
    public static partial IQueryable<ProductBrandListDto> MapProductBrandToListDto(this IQueryable<ProductBrand> q);
}

[Mapper]
public partial class ProductBrandMapper
{
    public partial ProductBrand AdminInputToProductBrand(ProductBrandAdminInputDto input);
    public partial void AdminInputToProductBrand(ProductBrandAdminInputDto input, ProductBrand ProductBrand);
}