using Common.Commons;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public static class ProductQuery
{
    public static async Task<List<ProductListDto>> GetVisibleProducts(this IProductRepo productRepo, CancellationToken ct = default)
    {
        return await productRepo.FilterQuery(new ProductListFilterDto()
        {
            Visible = true,
        }).MapProductToListDto().ToListAsync(ct);
    }

    public static async Task<PaginatedList<ProductListDto>> GetAdminListDto(this IProductRepo productRepo, ProductListFilterDto filter, CancellationToken ct = default)
    {
        var query = productRepo.FilterQuery(filter: filter)
            .OrderByDescending(x => x.ViewOrder).OrderByDescending(x => x.Id);

        return await PaginatedList<ProductListDto>.CreateAsync(
            source: query.MapProductToListDto(),
            filter: filter,
            ct: ct);
    }

    public static async Task<ProductDetailDto?> GetByIdAdminDto(this IProductRepo productRepo, IdDto routeVal, CancellationToken ct = default)
    {
        var query = productRepo.FilterQuery(routeVal.Id);
        return await query.MapProductToDetailDto().FirstOrDefaultAsync(ct);
    }
}