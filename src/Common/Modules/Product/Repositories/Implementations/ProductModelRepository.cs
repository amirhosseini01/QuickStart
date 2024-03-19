using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductModelRepository : GenericRepository<ProductModel>, IProductModelRepository
{
    private readonly DbSet<ProductModel> _entities;
    public ProductModelRepository(ApiDbContext context) : base(context) => _entities = context.ProductModels;

    public async Task<PaginatedList<ProductModelListDto>> GetProductModelList(ProductModelListFilterDto filter, CancellationToken ct = default)
    {
        var query = _entities.AsNoTracking();
        return await PaginatedList<ProductModelListDto>.CreateAsync(source: query.MapProductModelToListDto(), filter: filter, ct: ct);
    }
    public async Task<ProductModelDetailDto?> GetProductModel(int id, CancellationToken ct = default)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return await query.MapProductModelToDetailDto().FirstOrDefaultAsync(ct);
    }
}