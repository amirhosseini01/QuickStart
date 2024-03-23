using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductStockRepo : GenericRepository<ProductStock>, IProductStockRepo
{
    private readonly DbSet<ProductStock> _entities;
    public ProductStockRepo(BaseDbContext context) : base(context) => _entities = context.ProductStocks;

    public IQueryable<ProductStock> FilterQuery(ProductStockListFilterDto? filter = null)
    {
        var query = _entities.AsNoTracking();

        if (filter is null)
        {
            return query;
        }

        return query;
    }
    public IQueryable<ProductStock> FilterQuery(int id)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return query;
    }
}