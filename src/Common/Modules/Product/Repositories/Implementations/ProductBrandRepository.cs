using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductBrandRepository : GenericRepository<ProductBrand>, IProductBrandRepository
{
    private readonly DbSet<ProductBrand> _entities;
    public ProductBrandRepository(ApiDbContext context) : base(context) => _entities = context.ProductBrands;

    public async Task<PaginatedList<ProductBrandListDto>> GetProductBrandList(ProductBrandListFilterDto filter, CancellationToken ct = default)
    {
        var query = _entities.AsNoTracking();
        return await PaginatedList<ProductBrandListDto>.CreateAsync(source: query.MapProductBrandToListDto(), filter: filter, ct: ct);
    }
    public async Task<ProductBrandDetailDto?> GetProductBrand(int id, CancellationToken ct = default)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return await query.MapProductBrandToDetailDto().FirstOrDefaultAsync(ct);
    }
}