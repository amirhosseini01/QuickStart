using Common.Commons;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public static class ProductStockQuery
{
    public static async Task<List<ProductStockListDto>> GetVisibleProductStocks(this IProductStockRepo productStockRepo, CancellationToken ct = default)
    {
        return await productStockRepo.FilterQuery().MapProductStockToListDto().ToListAsync(ct);
    }

    public static async Task<PaginatedList<ProductStockListDto>> GetAdminListDto(this IProductStockRepo productStockRepo, ProductStockListFilterDto filter, CancellationToken ct = default)
    {
        var query = productStockRepo.FilterQuery(filter: filter).OrderByDescending(x => x.Id);

        return await PaginatedList<ProductStockListDto>.CreateAsync(
            source: query.MapProductStockToListDto(),
            filter: filter,
            ct: ct);
    }

    public static async Task<ProductStockDetailDto?> GetByIdAdminDto(this IProductStockRepo productStockRepo, IdDto routeVal, CancellationToken ct = default)
    {
        var query = productStockRepo.FilterQuery(routeVal.Id);
        return await query.MapProductStockToDetailDto().FirstOrDefaultAsync(ct);
    }
}