using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductBrandRepo : GenericRepository<ProductBrand>, IProductBrandRepo
{
    private readonly DbSet<ProductBrand> _entities;
    public ProductBrandRepo(BaseDbContext context) : base(context) => _entities = context.ProductBrands;

    public IQueryable<ProductBrand> FilterQuery(ProductBrandListFilterDto filter)
    {
        var query = _entities.AsNoTracking();

        if (filter.Visible is not null)
        {
            query = query.Where(x => x.Visible == filter.Visible.Value);
        }

        return query;
    }
    public IQueryable<ProductBrand> FilterQuery(int id)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return query;
    }
}