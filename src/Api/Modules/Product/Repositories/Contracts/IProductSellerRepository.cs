using Api.Common;

namespace Api.Modules.Product;

public interface IProductSellerRepository: IGenericRepository<ProductSeller>
{
    Task<PaginatedList<ProductSellerListDto>> GetProductSellerList(ProductSellerListFilterDto filter, CancellationToken cancellationToken = default);
    Task<ProductSellerDetailDto?> GetProductSeller(int id, CancellationToken cancellationToken = default);
}