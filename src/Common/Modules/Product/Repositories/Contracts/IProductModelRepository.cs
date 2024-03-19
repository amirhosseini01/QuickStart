using Common.Commons;

namespace Common.Modules.Product;

public interface IProductModelRepository : IGenericRepository<ProductModel>
{
    Task<PaginatedList<ProductModelListDto>> GetProductModelList(ProductModelListFilterDto filter, CancellationToken ct = default);
    Task<ProductModelDetailDto?> GetProductModel(int id, CancellationToken ct = default);
}