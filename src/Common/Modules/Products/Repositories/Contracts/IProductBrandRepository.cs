using Common.Commons;

namespace Common.Modules.Product;

public interface IProductBrandRepository : IGenericRepository<ProductBrand>
{
    Task<PaginatedList<ProductBrandListDto>> GetProductBrandList(ProductBrandListFilterDto filter, CancellationToken cancellationToken = default);
    Task<ProductBrandDetailDto?> GetProductBrand(int id, CancellationToken cancellationToken = default);
}