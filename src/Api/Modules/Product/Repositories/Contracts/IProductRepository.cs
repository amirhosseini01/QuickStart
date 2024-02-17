using Api.Modules.Shared;

namespace Api.Modules.Product;

public interface IProductRepository: IGenericRepository<Product>
{
    Task<List<ProductListVm>> GetProductLists(PaginatedListFilter filter, CancellationToken cancellationToken = default);
}