using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductRepo : GenericRepository<Product>, IProductRepo
{
    private readonly DbSet<Product> _entities;
    public ProductRepo(ApiDbContext context) : base(context) => _entities = context.Products;

    public IQueryable<Product> FilterQuery(ProductListFilterDto filter)
    {
        var query = _entities.AsNoTracking();

        if (filter.Visible is not null)
        {
            query = query.Where(x => x.Visible == filter.Visible.Value);
        }

        if (filter.Saleable is not null)
        {
            query = query.Where(x => x.Saleable == filter.Saleable.Value);
        }

        return query;
    }
    public IQueryable<Product> FilterQuery(int id)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return query;
    }
}