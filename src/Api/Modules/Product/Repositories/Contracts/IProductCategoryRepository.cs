using Api.Common;

namespace Api.Modules.Product;

public interface IProductCategoryRepository: IGenericRepository<ProductCategory>
{
    Task<PaginatedList<ProductCategoryListDto>> GetProductCategoryList(ProductCategoryListFilterDto filter, CancellationToken cancellationToken = default);
    Task<ProductCategoryDetailDto?> GetProductCategory(int id, CancellationToken cancellationToken = default);
}