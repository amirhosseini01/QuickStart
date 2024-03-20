using Common.Commons;

namespace Common.Modules.Product;

public interface IProductModelRepo : IGenericRepository<ProductModel>
{
    IQueryable<ProductModel> FilterQuery(ProductModelListFilterDto? filter = null);
    IQueryable<ProductModel> FilterQuery(int id);
}