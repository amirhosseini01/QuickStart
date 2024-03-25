using Common.Commons;

namespace Common.Modules.Product;

public interface IProductCategoryRepo : IGenericRepository<ProductCategory>
{
    IQueryable<ProductCategory> FilterQuery(ProductCategoryListFilterDto? filter = null);
    IQueryable<ProductCategory> FilterQuery(int id);
}