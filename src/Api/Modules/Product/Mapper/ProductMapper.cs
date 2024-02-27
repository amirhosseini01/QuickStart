namespace Api.Modules.Product;

public static class ProductMapper
{
    public static IQueryable<ProductListDto> MapProductList(this IQueryable<Product> query) =>
        query.Select(x => new ProductListDto
        {
            Id = x.Id,
            Thumbnail = x.Thumbnail,
            Title = x.Title,
            Saleable = x.Saleable,
            Price = x.ProductModels.Select(xx => xx.Price).OrderBy(xx => xx).FirstOrDefault()
        });


    public static IQueryable<ProductDetailDto> MapProductDetail(this IQueryable<Product> query) =>
    query.Select(x => new ProductDetailDto
    {
        Id = x.Id,
        Title = x.Title,
        Image = x.Image,
        Saleable = x.Saleable,
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
        ProductModels = x.ProductModels.Select(xx=> new ProductProductModelDto
        {
            ViewOrder = xx.ViewOrder,
            Type = xx.Type,
            Title = xx.Title,
            Price = xx.Price,
            Value = xx.Value,
        }).ToList(),
    });
}