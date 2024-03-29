using Riok.Mapperly.Abstractions;

namespace Common.Modules.Product;

[Mapper]
public static partial class ProductCategoryMapperQuery
{
    public static partial IQueryable<ProductCategoryAdminDetailDto> MapProductCategoryToDetailDto(this IQueryable<ProductCategory> q);
    public static partial IQueryable<ProductCategoryAdminListDto> MapProductCategoryToAdminListDto(this IQueryable<ProductCategory> q);
    public static partial IQueryable<ProductCategoryListDto> MapProductCategoryToListDto(this IQueryable<ProductCategory> q);
}

[Mapper]
public partial class ProductCategoryMapper
{
    [MapperIgnoreSource(nameof(ProductCategory.Image))]
    public partial ProductCategory AdminInputToProductCategory(ProductCategoryAdminInputDto input);

    [MapperIgnoreSource(nameof(ProductCategory.Image))]
    public partial void AdminInputToProductCategory(ProductCategoryAdminInputDto input, ProductCategory productCategory);
}