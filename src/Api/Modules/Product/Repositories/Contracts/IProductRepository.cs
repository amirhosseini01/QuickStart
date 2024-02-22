using Api.Common;

namespace Api.Modules.Product;

public interface IProductRepository: IGenericRepository<Product>
{
    Task<PaginatedList<ProductListDto>> GetProductLists(ProductListFilterDto filter, CancellationToken cancellationToken = default);
}