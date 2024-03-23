using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductSellerRepo : GenericRepository<ProductSeller>, IProductSellerRepo
{
    private readonly DbSet<ProductSeller> _entities;
    public ProductSellerRepo(BaseDbContext context) : base(context) => _entities = context.ProductSellers;

    public IQueryable<ProductSeller> FilterQuery(ProductSellerListFilterDto? filter = null)
    {
        var query = _entities.AsNoTracking();

        if (filter is null)
        {
            return query;
        }

        if (filter.UserId is not null)
        {
            return query.Where(x => x.UserId == filter.UserId);
        }

        return query;
    }
    public IQueryable<ProductSeller> FilterQuery(int id)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return query;
    }
}