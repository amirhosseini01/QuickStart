using Common.Commons;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public static class ProductCategoryQuery
{
    public static async Task<List<ProductCategoryListDto>> GetVisibleCategories(this IProductCategoryRepo productCategoryRepo, CancellationToken ct = default)
    {
        return await productCategoryRepo.FilterQuery(new ProductCategoryListFilterDto()
        {
            Visible = true,
        }).MapProductCategoryToListDto().ToListAsync(ct);
    }

    public static async Task<PaginatedList<ProductCategoryListDto>> GetAdminList(this IProductCategoryRepo productCategoryRepo, ProductCategoryListFilterDto filter, CancellationToken ct = default)
    {
        var query = productCategoryRepo.FilterQuery(filter: filter)
            .OrderByDescending(x => x.ViewOrder).OrderByDescending(x => x.Id);

        return await PaginatedList<ProductCategoryListDto>.CreateAsync(
            source: query.MapProductCategoryToListDto(),
            filter: filter,
            ct: ct);
    }

    public static async Task<ProductCategoryDetailDto?> GetByIdAdminDto(this IProductCategoryRepo productCategoryRepo, IdDto routeVal, CancellationToken ct = default)
    {
        var query = productCategoryRepo.FilterQuery(routeVal.Id);
        return await query.MapProductCategoryToDetailDto().FirstOrDefaultAsync(ct);
    }
}