using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductCategoryRepo : GenericRepository<ProductCategory>, IProductCategoryRepo
{
    private readonly DbSet<ProductCategory> _entities;
    public ProductCategoryRepo(BaseDbContext context) : base(context) =>
        _entities = context.ProductCategories;

    public IQueryable<ProductCategory> FilterQuery(ProductCategoryListFilterDto? filter = null)
    {
        var query = _entities.AsNoTracking();

        if (filter is null)
        {
            return query;
        }

        if (filter.Visible is not null)
        {
            query = query.Where(x => x.Visible == filter.Visible.Value);
        }

        return query;
    }

    public IQueryable<ProductCategory> FilterQuery(int id) => _entities.AsNoTracking().Where(x => x.Id == id);
}