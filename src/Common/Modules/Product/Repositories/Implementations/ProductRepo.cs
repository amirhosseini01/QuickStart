using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductRepo : GenericRepository<Product>, IProductRepo
{
    private readonly DbSet<Product> _entities;
    public ProductRepo(BaseDbContext context) : base(context) => _entities = context.Products;

    public IQueryable<Product> FilterQuery(ProductListFilterDto? filter = null)
    {
        var query = _entities.AsNoTracking();

        if (filter is null)
        {
            return query;
        }

        if (filter.Visible is not null)
        {
            query = query.Where(x => x.Visible == filter.Visible.Value);
            query = query.Where(x => x.ProductCategory.Visible == filter.Visible.Value);
            query = query.Where(x => x.ProductBrand.Visible == filter.Visible.Value);
        }

        if (filter.Saleable is not null)
        {
            query = query.Where(x => x.Saleable == filter.Saleable.Value);
        }

        if (filter.IsSpecialOffer is not null)
        {
            query = query.Where(x => x.IsSpecialOffer == filter.IsSpecialOffer.Value);
        }

        return query;
    }
    public IQueryable<Product> FilterQuery(int id)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return query;
    }
}