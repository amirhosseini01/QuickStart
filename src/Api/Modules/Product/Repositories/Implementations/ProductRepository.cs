using Api.Common;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Product;

public class ProductRepository: GenericRepository<Product>, IProductRepository
{
    private readonly DbSet<Product> _entities;
	public ProductRepository(ApiDbContext context) : base(context) => _entities = context.Products;

	public async Task<PaginatedList<ProductListDto>> GetProductLists(ProductListFilterDto filter, CancellationToken cancellationToken = default) 
    {
        var query = _entities.AsNoTracking();

        if(filter.Visible is not null)
        {
            query = query.Where(x=> x.Visible == filter.Visible.Value);
        }

        if(filter.Saleable is not null)
        {
            query = query.Where(x=> x.Saleable == filter.Saleable.Value);
        }

        return await PaginatedList<ProductListDto>.CreateAsync(source: query.MapProductList(), filter: filter, cancellationToken: cancellationToken);
    }
    public async Task<ProductDetailDto?> GetProduct(int id, CancellationToken cancellationToken = default) 
    {
        var query = _entities.AsNoTracking().Where(x=> x.Id == id);
        return await query.MapProductDetail().FirstOrDefaultAsync(cancellationToken);
    }
}