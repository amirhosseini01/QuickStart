using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductModelRepo : GenericRepository<ProductModel>, IProductModelRepo
{
    private readonly DbSet<ProductModel> _entities;
    public ProductModelRepo(ApiDbContext context) : base(context) => _entities = context.ProductModels;

    public IQueryable<ProductModel> FilterQuery(ProductModelListFilterDto? filter = null)
    {
        var query = _entities.AsNoTracking();

        if (filter is null)
        {
            return query;
        }

        if (filter.ProductId > 0)
        {
            query = query.Where(x => x.ProductId == filter.ProductId);
        }

        return query;
    }
    public IQueryable<ProductModel> FilterQuery(int id)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return query;
    }
}