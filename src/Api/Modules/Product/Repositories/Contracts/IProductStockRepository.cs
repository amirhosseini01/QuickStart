using Api.Common;

namespace Api.Modules.Product;

public interface IProductStockRepository: IGenericRepository<ProductStock>
{
    Task<PaginatedList<ProductStockListDto>> GetProductStockList(ProductStockListFilterDto filter, CancellationToken cancellationToken = default);
    Task<ProductStockDetailDto?> GetProductStock(int id, CancellationToken cancellationToken = default);
}