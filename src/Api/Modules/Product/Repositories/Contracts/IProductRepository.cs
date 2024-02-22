using Api.Modules.Shared;

namespace Api.Modules.Product;

public interface IProductRepository: IGenericRepository<Product>
{
    Task<List<ProductListVm>> GetProductLists(ProductListFilter filter, CancellationToken cancellationToken = default);
}