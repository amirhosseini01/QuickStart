using Riok.Mapperly.Abstractions;

namespace Api.Modules.Product;

[Mapper]
public static partial class ProductCategoryMapperQuery
{
    public static partial IQueryable<ProductCategoryDetailDto> MapProductCategoryToDetailDto(this IQueryable<ProductCategory> q);
    public static partial IQueryable<ProductCategoryListDto> MapProductCategoryToListDto(this IQueryable<ProductCategory> q);
}

[Mapper]
public partial class ProductCategoryMapper
{
    public partial ProductCategory AdminInputToProductCategory(ProductCategoryAdminInputDto input);
    public partial void AdminInputToProductCategory(ProductCategory productCategory, ProductCategoryAdminInputDto input);
}