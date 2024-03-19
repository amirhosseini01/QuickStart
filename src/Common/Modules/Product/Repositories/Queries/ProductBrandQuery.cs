using Common.Commons;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public static class ProductBrandQuery
{
    public static async Task<List<ProductBrandListDto>> GetVisibleBrands(this IProductBrandRepo productBrandRepo, CancellationToken ct = default)
    {
        return await productBrandRepo.FilterQuery(new ProductBrandListFilterDto()
        {
            Visible = true,
        }).MapProductBrandToListDto().ToListAsync(ct);
    }

    public static async Task<PaginatedList<ProductBrandListDto>> GetAdminList(this IProductBrandRepo productBrandRepo, ProductBrandListFilterDto filter, CancellationToken ct = default)
    {
        var query = productBrandRepo.FilterQuery(filter: filter)
            .OrderByDescending(x => x.ViewOrder).OrderByDescending(x => x.Id);

        return await PaginatedList<ProductBrandListDto>.CreateAsync(
            source: query.MapProductBrandToListDto(),
            filter: filter,
            ct: ct);
    }

    public static async Task<ProductBrandDetailDto?> GetByIdAdminDto(this IProductBrandRepo productBrandRepo, IdDto routeVal, CancellationToken ct = default)
    {
        var query = productBrandRepo.FilterQuery(routeVal.Id);
        return await query.MapProductBrandToDetailDto().FirstOrDefaultAsync(ct);
    }
}