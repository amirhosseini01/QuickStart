using Common.Commons;

namespace Common.Modules.Product;

public interface IProductStockRepository : IGenericRepository<ProductStock>
{
    Task<PaginatedList<ProductStockListDto>> GetProductStockList(ProductStockListFilterDto filter, CancellationToken ct = default);
    Task<ProductStockDetailDto?> GetProductStock(int id, CancellationToken ct = default);
}