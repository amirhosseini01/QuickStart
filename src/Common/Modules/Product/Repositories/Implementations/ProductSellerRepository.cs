using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductSellerRepository : GenericRepository<ProductSeller>, IProductSellerRepository
{
    private readonly DbSet<ProductSeller> _entities;
    public ProductSellerRepository(ApiDbContext context) : base(context) => _entities = context.ProductSellers;

    public async Task<PaginatedList<ProductSellerListDto>> GetProductSellerList(ProductSellerListFilterDto filter, CancellationToken ct = default)
    {
        var query = _entities.AsNoTracking();
        return await PaginatedList<ProductSellerListDto>.CreateAsync(source: query.MapProductSellerToListDto(), filter: filter, ct: ct);
    }
    public async Task<ProductSellerDetailDto?> GetProductSeller(int id, CancellationToken ct = default)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return await query.MapProductSellerToDetailDto().FirstOrDefaultAsync(ct);
    }
}