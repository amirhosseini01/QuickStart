using Api.Common;

namespace Api.Modules.Product;

public interface IProductCategoryRepository: IGenericRepository<ProductCategory>
{
    IQueryable<ProductCategory> FilterQuery(ProductCategoryListFilterDto filter);
    IQueryable<ProductCategory> FilterQuery(int id);
}