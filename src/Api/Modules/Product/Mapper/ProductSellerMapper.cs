using Riok.Mapperly.Abstractions;

namespace Api.Modules.Product;

[Mapper]
public static partial class ProductSellerMapperQuery
{
    public static partial IQueryable<ProductSellerDetailDto> MapProductSellerToDetailDto(this IQueryable<ProductSeller> q);
    public static partial IQueryable<ProductSellerListDto> MapProductSellerToListDto(this IQueryable<ProductSeller> q);
}

[Mapper]
public partial class ProductSellerMapper
{
    public partial ProductSeller AdminInputToProductSeller(ProductSellerAdminInputDto input);

    [MapperIgnoreSource(nameof(ProductSeller.Logo))]
    public partial void AdminInputToProductSeller(ProductSellerAdminInputDto input, ProductSeller ProductSeller);
}