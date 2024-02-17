using Api.Modules.Shared;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Product;

public class ProductRepository: GenericRepository<Product>, IProductRepository
{
    private readonly DbSet<Product> _entities;
	public ProductRepository(ApiDbContext context) : base(context) => _entities = context.Products;

	public async Task<List<ProductListVm>> GetProductLists(PaginatedListFilter filter, CancellationToken cancellationToken = default) 
    {
        var query = _entities.AsNoTracking();

        return await query.MapProductList().ToListAsync(cancellationToken);
    }
}