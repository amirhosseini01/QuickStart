using Common.Commons;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public static class ProductSellerQuery
{
    public static async Task<List<ProductSellerListDto>> GetVisibleProductSellers(this IProductSellerRepo productSellerRepo, CancellationToken ct = default)
    {
        return await productSellerRepo.FilterQuery().MapProductSellerToListDto().ToListAsync(ct);
    }

    public static async Task<PaginatedList<ProductSellerListDto>> GetAdminListDto(this IProductSellerRepo productSellerRepo, ProductSellerListFilterDto filter, CancellationToken ct = default)
    {
        var query = productSellerRepo.FilterQuery(filter: filter).OrderByDescending(x => x.Id);

        return await PaginatedList<ProductSellerListDto>.CreateAsync(
            source: query.MapProductSellerToListDto(),
            filter: filter,
            ct: ct);
    }

    public static async Task<ProductSellerDetailDto?> GetByIdAdminDto(this IProductSellerRepo productSellerRepo, IdDto routeVal, CancellationToken ct = default)
    {
        var query = productSellerRepo.FilterQuery(routeVal.Id);
        return await query.MapProductSellerToDetailDto().FirstOrDefaultAsync(ct);
    }
}