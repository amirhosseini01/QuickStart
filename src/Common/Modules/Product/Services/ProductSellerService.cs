using Common.Commons;

namespace Common.Modules.Product;

public class ProductSellerService
{
    private readonly IProductSellerRepo _productSellerRepo;
    private readonly FileUploader _fileUploader;
    public ProductSellerService(IProductSellerRepo productSellerRepository, FileUploader fileUploader)
    {
        _productSellerRepo = productSellerRepository;
        _fileUploader = fileUploader;
    }

    public async Task<PaginatedList<ProductSellerListDto>> GetAdminListDto(ProductSellerListFilterDto filter, CancellationToken ct = default)
    {
        return await _productSellerRepo.GetAdminListDto(filter: filter, ct: ct);
    }

    public async Task<ProductSellerDetailDto?> GetByIdAdminDto(IdDto routeVal, CancellationToken ct = default)
    {
        return await _productSellerRepo.GetByIdAdminDto(routeVal: routeVal, ct: ct);
    }

    public async Task<ProductSeller?> GetByIdAdmin(IdDto routeVal, CancellationToken ct = default)
    {
        return await _productSellerRepo.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
    }

    public async Task Add(ProductSellerAdminInputDto input, CancellationToken ct = default)
    {
        var ProductSeller = new ProductSellerMapper().AdminInputToProductSeller(input);

        if (input.Logo is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Logo);
            ProductSeller.Logo = uploadRes;
        }

        await _productSellerRepo.AddAsync(ProductSeller, ct);
        await _productSellerRepo.SaveChangesAsync(ct);
    }

    public async Task Update(ProductSeller productSeller, ProductSellerAdminInputDto input, CancellationToken ct = default)
    {
        new ProductSellerMapper().AdminInputToProductSeller(input, productSeller);

        if (input.Logo is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Logo);
            productSeller.Logo = uploadRes;
        }

        await _productSellerRepo.SaveChangesAsync(ct);
    }

    public async Task Remove(ProductSeller productSeller, CancellationToken ct = default)
    {
        _productSellerRepo.Remove(productSeller);
        await _productSellerRepo.SaveChangesAsync(ct);
    }
}