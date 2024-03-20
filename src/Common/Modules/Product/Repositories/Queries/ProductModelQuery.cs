using Common.Commons;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public static class ProductModelQuery
{
    public static async Task<List<ProductModelListDto>> GetVisibleProductModels(this IProductModelRepo productModelRepo, CancellationToken ct = default)
    {
        return await productModelRepo.FilterQuery().MapProductModelToListDto().ToListAsync(ct);
    }

    public static async Task<PaginatedList<ProductModelListDto>> GetAdminListDto(this IProductModelRepo productModelRepo, ProductModelListFilterDto filter, CancellationToken ct = default)
    {
        var query = productModelRepo.FilterQuery(filter: filter)
            .OrderByDescending(x => x.ViewOrder).OrderByDescending(x => x.Id);

        return await PaginatedList<ProductModelListDto>.CreateAsync(
            source: query.MapProductModelToListDto(),
            filter: filter,
            ct: ct);
    }

    public static async Task<ProductModelDetailDto?> GetByIdAdminDto(this IProductModelRepo productModelRepo, IdDto routeVal, CancellationToken ct = default)
    {
        var query = productModelRepo.FilterQuery(routeVal.Id);
        return await query.MapProductModelToDetailDto().FirstOrDefaultAsync(ct);
    }
}