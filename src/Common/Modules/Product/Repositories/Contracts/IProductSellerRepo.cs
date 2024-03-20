using Common.Commons;

namespace Common.Modules.Product;

public interface IProductSellerRepo : IGenericRepository<ProductSeller>
{
    IQueryable<ProductSeller> FilterQuery(ProductSellerListFilterDto? filter = null);
    IQueryable<ProductSeller> FilterQuery(int id);
}