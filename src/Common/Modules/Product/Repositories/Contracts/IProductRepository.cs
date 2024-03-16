using Common.Commons;

namespace Common.Modules.Product;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<PaginatedList<ProductListDto>> GetProductLists(ProductListFilterDto filter, CancellationToken cancellationToken = default);
    Task<ProductDetailDto?> GetProduct(int id, CancellationToken cancellationToken = default);
}