using Common.Commons;

namespace Common.Modules.Product;

public interface IProductStockRepo : IGenericRepository<ProductStock>
{
    IQueryable<ProductStock> FilterQuery(ProductStockListFilterDto? filter = null);
    IQueryable<ProductStock> FilterQuery(int id);
}