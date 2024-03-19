using Common.Commons;

namespace Common.Modules.Product;

public interface IProductRepo : IGenericRepository<Product>
{
    IQueryable<Product> FilterQuery(ProductListFilterDto filter);
    IQueryable<Product> FilterQuery(int id);
}