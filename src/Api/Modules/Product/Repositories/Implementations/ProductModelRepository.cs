using Api.Common;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Product;

public class ProductModelRepository: GenericRepository<ProductModel>, IProductModelRepository
{
    private readonly DbSet<ProductModel> _entities;
	public ProductModelRepository(ApiDbContext context) : base(context) => _entities = context.ProductModels;

	public async Task<PaginatedList<ProductModelListDto>> GetProductModelList(ProductModelListFilterDto filter, CancellationToken cancellationToken = default) 
    {
        var query = _entities.AsNoTracking();
        return await PaginatedList<ProductModelListDto>.CreateAsync(source: query.MapProductModelToListDto(), filter: filter, cancellationToken: cancellationToken);
    }
    public async Task<ProductModelDetailDto?> GetProductModel(int id, CancellationToken cancellationToken = default) 
    {
        var query = _entities.AsNoTracking().Where(x=> x.Id == id);
        return await query.MapProductModelToDetailDto().FirstOrDefaultAsync(cancellationToken);
    }
}