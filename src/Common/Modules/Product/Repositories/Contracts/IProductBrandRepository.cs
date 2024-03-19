using Common.Commons;

namespace Common.Modules.Product;

public interface IProductBrandRepository : IGenericRepository<ProductBrand>
{
    Task<PaginatedList<ProductBrandListDto>> GetProductBrandList(ProductBrandListFilterDto filter, CancellationToken ct = default);
    Task<ProductBrandDetailDto?> GetProductBrand(int id, CancellationToken ct = default);
}