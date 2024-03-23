using Common.Commons;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public static class ProductQuery
{
    public static async Task<PaginatedList<ProductAdminListDto>> GetAdminListDto(this IProductRepo productRepo, ProductListFilterDto filter, CancellationToken ct = default)
    {
        var query = productRepo.FilterQuery(filter: filter)
            .OrderByDescending(x => x.ViewOrder).ThenByDescending(x => x.Id);

        return await PaginatedList<ProductAdminListDto>.CreateAsync(
            source: query.MapProductToAdminListDto(),
            filter: filter,
            ct: ct);
    }

    public static async Task<ProductDetailDto?> GetByIdAdminDto(this IProductRepo productRepo, IdDto routeVal, CancellationToken ct = default)
    {
        var query = productRepo.FilterQuery(routeVal.Id);
        return await query.MapProductToDetailDto().FirstOrDefaultAsync(ct);
    }

    public static async Task<List<ProductListDto>> GetSpecialOffers(this IProductRepo productRepo, CancellationToken ct = default)
    {
        var query = productRepo.FilterQuery(filter: new()
        {
            Saleable = true,
            Visible = true,
            IsSpecialOffer = true,
        });
        return await query.MapProductToListDto()
            .OrderByDescending(x => x.ViewOrder).ThenByDescending(x => x.Id).ToListAsync(ct);
    }

    public static async Task<List<ProductListDto>> GetTopDiscounts(this IProductRepo productRepo, CancellationToken ct = default)
    {
        var query = productRepo.FilterQuery(filter: new()
        {
            Saleable = true,
            Visible = true,
        });
        return await query.MapProductToListDto()
            .OrderByDescending(x => x.Discount).ThenBy(x => x.Price)
            .ThenByDescending(x => x.ViewOrder).ThenByDescending(x => x.Id)
        .ToListAsync(ct);
    }

    public static async Task<List<ProductListDto>> GetSuggested(this IProductRepo productRepo, CancellationToken ct = default)
    {
        var query = productRepo.FilterQuery(filter: new()
        {
            Saleable = true,
            Visible = true,
        });
        return await query.MapProductToListDto()
            .OrderBy(x => x.ViewOrder).ThenByDescending(x => x.Id)
        .ToListAsync(ct);
    }

    public static async Task<List<ProductTopSaleListDto>> GetTopSales(this IProductRepo productRepo, CancellationToken ct = default)
    {
        var query = productRepo.FilterQuery(filter: new()
        {
            Saleable = true,
            Visible = true,
        });
        return await query.MapProductToSaleListDto()
            .OrderBy(x => x.Discount).ThenBy(x => x.Price).ThenByDescending(x => x.Id)
        .ToListAsync(ct);
    }
    public static async Task<List<ProductToViewListDto>> GetTopViewCount(this IProductRepo productRepo, CancellationToken ct = default)
    {
        var query = productRepo.FilterQuery(filter: new()
        {
            Saleable = true,
            Visible = true,
        });
        return await query.MapProductToTopViewedDto()
            .OrderBy(x => x.ViewOrder)
        .ToListAsync(ct);
    }
}