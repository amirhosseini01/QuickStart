using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductStockRepository : GenericRepository<ProductStock>, IProductStockRepository
{
    private readonly DbSet<ProductStock> _entities;
    public ProductStockRepository(ApiDbContext context) : base(context) => _entities = context.ProductStocks;

    public async Task<PaginatedList<ProductStockListDto>> GetProductStockList(ProductStockListFilterDto filter, CancellationToken cancellationToken = default)
    {
        var query = _entities.AsNoTracking();
        return await PaginatedList<ProductStockListDto>.CreateAsync(source: query.MapProductStockToListDto(), filter: filter, cancellationToken: cancellationToken);
    }
    public async Task<ProductStockDetailDto?> GetProductStock(int id, CancellationToken cancellationToken = default)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return await query.MapProductStockToDetailDto().FirstOrDefaultAsync(cancellationToken);
    }
}