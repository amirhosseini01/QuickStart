namespace Api.Modules.Product;

public static class ProductMapper
{
    public static IQueryable<ProductListDto> MapProductList(this IQueryable<Product> query) =>
        query.Select(x => new ProductListDto
        {
            Id = x.Id,
            Thumbnail = x.Thumbnail,
            Title = x.Title,
            // Price = x.ProductModels.Select(xx => xx.Price).Order().FirstOrDefault()
        });
}