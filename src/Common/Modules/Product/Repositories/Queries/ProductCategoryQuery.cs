using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public static class ProductCategoryQuery
{
    public static async Task<List<ProductCategoryListDto>> GetVisibleCategories(this IProductCategoryRepo productCategoryRepo, CancellationToken cancellationToken = default)
    {
        return await productCategoryRepo.FilterQuery(new ProductCategoryListFilterDto()
        {
            Visible = true,
        }).MapProductCategoryToListDto().ToListAsync(cancellationToken);
    }
}