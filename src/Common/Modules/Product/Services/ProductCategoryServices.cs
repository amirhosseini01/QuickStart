using Common.Commons;

namespace Common.Modules.Product;

public class ProductCategoryServices : IGenericService
{
    private readonly IProductCategoryRepo _productCategoryRepo;
    private readonly FileUploader _fileUploader;
    public ProductCategoryServices(IProductCategoryRepo productCategoryRepo, FileUploader fileUploader)
    {
        _productCategoryRepo = productCategoryRepo;
        _fileUploader = fileUploader;
    }

    public async Task<PaginatedList<ProductCategoryListDto>> GetAdminList(ProductCategoryListFilterDto filter, CancellationToken ct = default)
    {
        return await _productCategoryRepo.GetAdminList(filter: filter, ct: ct);
    }

    public async Task<ProductCategoryDetailDto?> GetByIdAdminDto(IdDto routeVal, CancellationToken ct = default)
    {
        return await _productCategoryRepo.GetByIdAdminDto(routeVal: routeVal, ct: ct);
    }
    public async Task<ProductCategory?> GetByIdAdmin(IdDto routeVal, CancellationToken ct) =>
        await _productCategoryRepo.FirstOrDefaultAsync(routeVal.Id, ct);

    public async Task<ProductCategory> Add(ProductCategoryAdminInputDto input, CancellationToken ct = default)
    {
        var entity = new ProductCategoryMapper().AdminInputToProductCategory(input);

        if (input.Image is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Image);
            entity.Image = uploadRes;
        }

        await _productCategoryRepo.AddAsync(entity, ct);
        await _productCategoryRepo.SaveChangesAsync(ct);

        return entity;
    }

    public async Task Update(ProductCategoryAdminInputDto input, ProductCategory entity, CancellationToken ct = default)
    {
        new ProductCategoryMapper().AdminInputToProductCategory(input, entity);

        if (input.Image is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Image);
            entity.Image = uploadRes;
        }

        await _productCategoryRepo.SaveChangesAsync(ct);
    }

    public async Task Remove(ProductCategory entity, CancellationToken ct = default)
    {
        _productCategoryRepo.Remove(entity);
        await _productCategoryRepo.SaveChangesAsync(ct);
    }
}