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

    public static async Task<PaginatedList<ProductListDto>> GetSpecialOffers(this IProductRepo productRepo, CancellationToken ct = default)
    {
        var filter = new ProductListFilterDto()
        {
            Saleable = true,
            Visible = true,
            IsSpecialOffer = true,
        };
        var query = productRepo.FilterQuery(filter: filter);
        return await PaginatedList<ProductListDto>.CreateAsync(source:
            query.MapProductToListDto()
            .OrderByDescending(x => x.ViewOrder).ThenByDescending(x => x.Id),
            filter: filter,
            ct: ct);
    }

    public static async Task<PaginatedList<ProductListDto>> GetTopDiscounts(this IProductRepo productRepo, CancellationToken ct = default)
    {
        var filter = new ProductListFilterDto()
        {
            Saleable = true,
            Visible = true,
        };
        var query = productRepo.FilterQuery(filter: filter);
        return await PaginatedList<ProductListDto>.CreateAsync(source:
            query.MapProductToListDto()
            .OrderByDescending(x => x.Discount).ThenBy(x => x.Price)
            .ThenByDescending(x => x.ViewOrder).ThenByDescending(x => x.Id),
            filter: filter,
            ct: ct);
    }

    public static async Task<PaginatedList<ProductListDto>> GetSuggested(this IProductRepo productRepo, CancellationToken ct = default)
    {
        var filter = new ProductListFilterDto()
        {
            Saleable = true,
            Visible = true,
        };
        var query = productRepo.FilterQuery(filter: filter);
        return await PaginatedList<ProductListDto>.CreateAsync(
            source: query.MapProductToListDto().OrderBy(x => x.ViewOrder).ThenByDescending(x => x.Id),
            filter: filter,
            ct: ct);
    }

    public static async Task<PaginatedList<ProductTopSaleListDto>> GetTopSales(this IProductRepo productRepo, CancellationToken ct = default)
    {
        var filter = new ProductListFilterDto()
        {
            Saleable = true,
            Visible = true,
        };
        var query = productRepo.FilterQuery(filter: filter);
        return await PaginatedList<ProductTopSaleListDto>.CreateAsync(
            source: query.MapProductToSaleListDto().OrderByDescending(x => x.SaleCount).ThenByDescending(x => x.Id),
            filter: filter,
            ct: ct);
    }
    public static async Task<PaginatedList<ProductToViewListDto>> GetTopViewCount(this IProductRepo productRepo, CancellationToken ct = default)
    {
        var filter = new ProductListFilterDto()
        {
            Saleable = true,
            Visible = true,
        };
        var query = productRepo.FilterQuery(filter: filter);
        return await PaginatedList<ProductToViewListDto>.CreateAsync(
            source: query.MapProductToTopViewedDto().OrderBy(x => x.ViewCount),
            filter: filter,
            ct: ct);
    }
}