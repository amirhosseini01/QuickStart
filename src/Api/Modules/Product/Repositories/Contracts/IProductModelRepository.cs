using Api.Common;

namespace Api.Modules.Product;

public interface IProductModelRepository: IGenericRepository<ProductModel>
{
    Task<PaginatedList<ProductModelListDto>> GetProductModelList(ProductModelListFilterDto filter, CancellationToken cancellationToken = default);
    Task<ProductModelDetailDto?> GetProductModel(int id, CancellationToken cancellationToken = default);
}