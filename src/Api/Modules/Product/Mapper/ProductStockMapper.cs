using Riok.Mapperly.Abstractions;

namespace Api.Modules.Product;

[Mapper]
public static partial class ProductStockMapperQuery
{
    public static partial IQueryable<ProductStockDetailDto> MapProductStockToDetailDto(this IQueryable<ProductStock> q);
    public static partial IQueryable<ProductStockListDto> MapProductStockToListDto(this IQueryable<ProductStock> q);
}

[Mapper]
public partial class ProductStockMapper
{
    public partial ProductStock AdminInputToProductStock(ProductStockAdminInputDto input);
    public partial void AdminInputToProductStock(ProductStockAdminInputDto input, ProductStock ProductStock);
}