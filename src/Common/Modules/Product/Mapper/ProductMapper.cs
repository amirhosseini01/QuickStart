using Riok.Mapperly.Abstractions;

namespace Common.Modules.Product;

public static class ProductManualMapper
{
    public static IQueryable<ProductAdminListDto> MapProductToAdminListDto(this IQueryable<Product> query) =>
        query.Select(x => new ProductAdminListDto
        {
            Id = x.Id,
            Thumbnail = x.Thumbnail,
            Title = x.Title,
            Saleable = x.Saleable,
            Visible = x.Visible,
        });

    public static IQueryable<ProductListDto> MapProductToListDto(this IQueryable<Product> query) =>
        query.Select(x => new ProductListDto
        {
            Id = x.Id,
            ViewOrder = x.ViewOrder,
            Thumbnail = x.Thumbnail,
            Title = x.Title,
            CategoryTitle = x.ProductCategory.Title,
            Discount = x.ProductModels.Select(xx => xx.Price).OrderBy(xx => xx).FirstOrDefault(),
            Price = x.ProductModels.Select(xx => xx.Price).OrderBy(xx => xx).FirstOrDefault()
        });
    public static IQueryable<ProductTopSaleListDto> MapProductToSaleListDto(this IQueryable<Product> query) =>
        query.Select(x => new ProductTopSaleListDto
        {
            Id = x.Id,
            ViewOrder = x.ViewOrder,
            Thumbnail = x.Thumbnail,
            Title = x.Title,
            CategoryTitle = x.ProductCategory.Title,
            Discount = x.ProductModels.Select(xx => xx.Price).OrderBy(xx => xx).FirstOrDefault(),
            Price = x.ProductModels.Select(xx => xx.Price).OrderBy(xx => xx).FirstOrDefault(),
            SaleCount = x.OrderDetails.Count()
        });

    public static IQueryable<ProductToViewListDto> MapProductToTopViewedDto(this IQueryable<Product> query) =>
        query.Select(x => new ProductToViewListDto
        {
            Id = x.Id,
            ViewOrder = x.ViewOrder,
            Thumbnail = x.Thumbnail,
            Title = x.Title,
            CategoryTitle = x.ProductCategory.Title,
            Discount = x.ProductModels.Select(xx => xx.Price).OrderBy(xx => xx).FirstOrDefault(),
            Price = x.ProductModels.Select(xx => xx.Price).OrderBy(xx => xx).FirstOrDefault(),
            ViewCount = x.ViewCount
        });



    public static IQueryable<ProductDetailDto> MapProductToDetailDto(this IQueryable<Product> query) =>
    query.Select(x => new ProductDetailDto
    {
        Id = x.Id,
        Title = x.Title,
        Image = x.Image,
        Saleable = x.Saleable,
        Visible = x.Visible,
        ShortDescription = x.ShortDescription,
        Description = x.Description,
        ProductSeller = new()
        {
            Id = x.ProductSellerId,
            Title = x.ProductSeller.Title,
            Logo = x.ProductSeller.Logo
        },
        ProductBrand = new()
        {
            Id = x.ProductBrandId,
            Title = x.ProductBrand.Title
        },
        ProductCategory = new()
        {
            Id = x.ProductCategoryId,
            Title = x.ProductCategory.Title
        },
        ProductModels = x.ProductModels.Select(xx => new ProductProductModelDto
        {
            ViewOrder = xx.ViewOrder,
            Type = xx.Type,
            Title = xx.Title,
            Price = xx.Price,
            Value = xx.Value,
        }).ToList(),
    });
}

[Mapper]
public partial class ProductMapper
{
    [MapperIgnoreSource(nameof(Product.Image))]
    [MapperIgnoreSource(nameof(Product.Thumbnail))]
    public partial Product AdminInputToProduct(ProductAdminInputDto input);

    [MapperIgnoreSource(nameof(Product.Image))]
    [MapperIgnoreSource(nameof(Product.Thumbnail))]
    public partial void AdminInputToProduct(ProductAdminInputDto input, Product product);
}