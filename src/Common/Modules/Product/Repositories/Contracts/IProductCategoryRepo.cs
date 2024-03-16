using Common.Commons;

namespace Common.Modules.Product;

public interface IProductCategoryRepo : IGenericRepository<ProductCategory>
{
    IQueryable<ProductCategory> FilterQuery(ProductCategoryListFilterDto filter);
    IQueryable<ProductCategory> FilterQuery(int id);
}