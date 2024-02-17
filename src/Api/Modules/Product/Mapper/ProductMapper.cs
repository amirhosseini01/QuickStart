namespace Api.Modules.Product;

public static class ProductMapper
{
    public static IQueryable<ProductListVm> MapProductList(this IQueryable<Product> query) =>
        query.Select(x => new ProductListVm
        {
            Id = x.Id,
            Thumbnail = x.Thumbnail,
            Title = x.Title,
            Price = x.ProductModels.Select(xx => xx.Price).Order().FirstOrDefault()
        });
}