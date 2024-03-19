using Common.Commons;

namespace Common.Modules.Product;

public class ProductService
{
    private readonly IProductRepo _productRepo;
    private readonly FileUploader _fileUploader;
    public ProductService(IProductRepo productRepository, FileUploader fileUploader)
    {
        _productRepo = productRepository;
        _fileUploader = fileUploader;
    }

    public async Task<PaginatedList<ProductListDto>> GetAdminListDto(ProductListFilterDto filter, CancellationToken ct = default)
    {
        return await _productRepo.GetAdminListDto(filter: filter, ct: ct);
    }

    public async Task<ProductDetailDto?> GetByIdAdminDto(IdDto routeVal, CancellationToken ct = default)
    {
        return await _productRepo.GetByIdAdminDto(routeVal: routeVal, ct: ct);
    }

    public async Task<Product?> GetByIdAdmin(IdDto routeVal, CancellationToken ct = default)
    {
        return await _productRepo.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
    }

    public async Task Add(ProductAdminInputDto input, CancellationToken ct = default)
    {
        var product = new ProductMapper().AdminInputToProduct(input);

        var imageUploadRes = await _fileUploader.UploadFile(input.Image);
        product.Image = imageUploadRes;

        var thumbnailUploadRes = await _fileUploader.UploadFile(input.Thumbnail);
        product.Thumbnail = thumbnailUploadRes;

        await _productRepo.AddAsync(product, ct);
        await _productRepo.SaveChangesAsync(ct);
    }

    public async Task Update(Product product, ProductAdminInputEditDto input, CancellationToken ct = default)
    {
        if (input.Image is not null)
        {
            var imageUploadRes = await _fileUploader.UploadFile(input.Image);
            product.Image = imageUploadRes;
        }

        if (input.Thumbnail is not null)
        {
            var thumbnailUploadRes = await _fileUploader.UploadFile(input.Thumbnail);
            product.Thumbnail = thumbnailUploadRes;
        }

        new ProductMapper().AdminInputToProduct(input, product);
        await _productRepo.SaveChangesAsync(ct);
    }

    public async Task Remove(Product product, CancellationToken ct = default)
    {
        _productRepo.Remove(product);
        await _productRepo.SaveChangesAsync(ct);
    }
}