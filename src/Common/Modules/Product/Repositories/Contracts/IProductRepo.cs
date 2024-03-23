using Common.Commons;

namespace Common.Modules.Product;

public interface IProductRepo : IGenericRepository<Product>
{
    IQueryable<Product> FilterQuery(ProductListFilterDto? filter = null);
    IQueryable<Product> FilterQuery(int id);
}