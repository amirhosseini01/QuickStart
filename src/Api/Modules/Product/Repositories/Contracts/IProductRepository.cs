using Api.Common;

namespace Api.Modules.Product;

public interface IProductRepository: IGenericRepository<Product>
{
    Task<PaginatedList<ProductListVm>> GetProductLists(ProductListFilter filter, CancellationToken cancellationToken = default);
}