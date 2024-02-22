using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Modules.Product;

public static class ProductsApi
{
    public static RouteGroupBuilder MapProducts(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/products");

        group.WithTags("Products");

        group.MapGet("/", async Task<Results<Ok<List<ProductListVm>>, EmptyHttpResult>> (ProductListFilter filter, IProductRepository ProductRepository, CancellationToken cancellationToken) =>
        {
            var products = await ProductRepository.GetProductLists(filter: filter, cancellationToken: cancellationToken);
            return TypedResults.Ok(products);
        });

        return group;
    }
}
