using Api.Common;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Product;

public class ProductCategoryRepository: GenericRepository<ProductCategory>, IProductCategoryRepository
{
    private readonly DbSet<ProductCategory> _entities;
	public ProductCategoryRepository(ApiDbContext context) : base(context) => _entities = context.ProductCategories;

	public async Task<PaginatedList<ProductCategoryListDto>> GetProductCategoryList(ProductCategoryListFilterDto filter, CancellationToken cancellationToken = default) 
    {
        var query = _entities.AsNoTracking();
        return await PaginatedList<ProductCategoryListDto>.CreateAsync(source: query.MapProductCategoryToListDto(), filter: filter, cancellationToken: cancellationToken);
    }
    public async Task<ProductCategoryDetailDto?> GetProductCategory(int id, CancellationToken cancellationToken = default) 
    {
        var query = _entities.AsNoTracking().Where(x=> x.Id == id);
        return await query.MapProductCategoryToDetailDto().FirstOrDefaultAsync(cancellationToken);
    }
}