using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Api;

public static class ProductsApi
{
    public static RouteGroupBuilder MapProducts(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/products");

        group.WithTags("Products");

        group.MapGet("/", async Task<Results<Ok, EmptyHttpResult>> (UserManager<AppUser> userManager) =>
        {
            return TypedResults.Ok();
        });

        return group;
    }
}
