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
        query = FilterVisibleProducts(query);
        return await PaginatedList<ProductListDto>.CreateAsync(source: query.MapProductList(), filter: filter, cancellationToken: cancellationToken);
    }
    public async Task<ProductDetailDto?> GetProduct(int id, CancellationToken cancellationToken = default) 
    {
        var query = _entities.AsNoTracking().Where(x=> x.Id == id);
        query = FilterVisibleProducts(query);
        return await query.MapProductDetail().FirstOrDefaultAsync(cancellationToken);
    }

    private static IQueryable<Product> FilterVisibleProducts(IQueryable<Product> query)
    {
        //todo: refactor this function to something like this: var anonymousType  = x=> x.visible
        query = query.Where(x=> x.Visible);
        query = query.Where(x=> x.ProductBrand.Visible);
        query = query.Where(x=> x.ProductCategory.Visible);
        return query;
    }
}