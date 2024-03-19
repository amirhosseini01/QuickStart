using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly DbSet<Product> _entities;
    public ProductRepository(ApiDbContext context) : base(context) => _entities = context.Products;

    public async Task<PaginatedList<ProductListDto>> GetProductLists(ProductListFilterDto filter, CancellationToken ct = default)
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

        return await PaginatedList<ProductListDto>.CreateAsync(source: query.MapProductList(), filter: filter, ct: ct);
    }
    public async Task<ProductDetailDto?> GetProduct(int id, CancellationToken ct = default)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return await query.MapProductDetail().FirstOrDefaultAsync(ct);
    }
}