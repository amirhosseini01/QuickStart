using Riok.Mapperly.Abstractions;

namespace Api.Modules.Product;

[Mapper]
public static partial class ProductModelMapperQuery
{
    public static partial IQueryable<ProductModelDetailDto> MapProductModelToDetailDto(this IQueryable<ProductModel> q);
    public static partial IQueryable<ProductModelListDto> MapProductModelToListDto(this IQueryable<ProductModel> q);
}

[Mapper]
public partial class ProductModelMapper
{
    public partial ProductModel AdminInputToProductModel(ProductModelAdminInputDto input);
    public partial void AdminInputToProductModel(ProductModelAdminInputDto input, ProductModel ProductModel);
}