using Common.Commons;

namespace Common.Modules.Product;

public interface IProductBrandRepo : IGenericRepository<ProductBrand>
{
    IQueryable<ProductBrand> FilterQuery(ProductBrandListFilterDto filter);
    IQueryable<ProductBrand> FilterQuery(int id);
}