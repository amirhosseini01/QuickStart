using Common.Commons;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Product;

public class ProductCategoryServices : IGenericService
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly FileUploader _fileUploader;
    public ProductCategoryServices(IProductCategoryRepository productCategoryRepository, FileUploader fileUploader)
    {
        _productCategoryRepository = productCategoryRepository;
        _fileUploader = fileUploader;
    }

    public async Task<PaginatedList<ProductCategoryListDto>> GetList(ProductCategoryListFilterDto filter, CancellationToken cancellationToken = default)
    {
        var query = _productCategoryRepository.FilterQuery(filter: filter)
            .OrderByDescending(x => x.ViewOrder).OrderByDescending(x => x.Id);

        return await PaginatedList<ProductCategoryListDto>.CreateAsync(
            source: query.MapProductCategoryToListDto(),
            filter: filter,
            cancellationToken: cancellationToken);
    }

    public async Task<ProductCategoryDetailDto?> GetByIdDto(IdDto routeVal, CancellationToken cancellationToken = default)
    {
        var query = _productCategoryRepository.FilterQuery(routeVal.Id);
        return await query.MapProductCategoryToDetailDto().FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<ProductCategory?> GetById(IdDto routeVal, CancellationToken cancellationToken) =>
        await _productCategoryRepository.FirstOrDefaultAsync(routeVal.Id, cancellationToken);

    public async Task<ProductCategory> Add(ProductCategoryAdminInputDto input, CancellationToken cancellationToken = default)
    {
        var entity = new ProductCategoryMapper().AdminInputToProductCategory(input);

        if (input.Image is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Image);
            entity.Image = uploadRes;
        }

        await _productCategoryRepository.AddAsync(entity, cancellationToken);
        await _productCategoryRepository.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task Update(ProductCategoryAdminInputDto input, ProductCategory entity, CancellationToken cancellationToken = default)
    {
        new ProductCategoryMapper().AdminInputToProductCategory(input, entity);

        if (input.Image is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Image);
            entity.Image = uploadRes;
        }

        await _productCategoryRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task Remove(ProductCategory entity, CancellationToken cancellationToken = default)
    {
        _productCategoryRepository.Remove(entity);
        await _productCategoryRepository.SaveChangesAsync(cancellationToken);
    }
}