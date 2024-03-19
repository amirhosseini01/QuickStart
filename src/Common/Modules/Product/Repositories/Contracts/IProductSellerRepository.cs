using Common.Commons;

namespace Common.Modules.Product;

public interface IProductSellerRepository : IGenericRepository<ProductSeller>
{
    Task<PaginatedList<ProductSellerListDto>> GetProductSellerList(ProductSellerListFilterDto filter, CancellationToken ct = default);
    Task<ProductSellerDetailDto?> GetProductSeller(int id, CancellationToken ct = default);
}