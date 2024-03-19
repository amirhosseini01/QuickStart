using Common.Commons;

namespace Common.Modules.Product;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<PaginatedList<ProductListDto>> GetProductLists(ProductListFilterDto filter, CancellationToken ct = default);
    Task<ProductDetailDto?> GetProduct(int id, CancellationToken ct = default);
}