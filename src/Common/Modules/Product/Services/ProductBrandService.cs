using Common.Commons;

namespace Common.Modules.Product;

public class ProductBrandService
{
    private readonly IProductBrandRepo _ProductBrandRepo;
    private readonly FileUploader _fileUploader;
    public ProductBrandService(IProductBrandRepo ProductBrandRepository,
        FileUploader fileUploader)
    {
        _ProductBrandRepo = ProductBrandRepository;
        _fileUploader = fileUploader;
    }

    public async Task<PaginatedList<ProductBrandListDto>> GetAdminList(ProductBrandListFilterDto filter, CancellationToken ct = default)
    {
        return await _ProductBrandRepo.GetAdminList(filter: filter, ct: ct);
    }

    public async Task<ProductBrandDetailDto?> GetByIdAdminDto(IdDto routeVal, CancellationToken ct = default)
    {
        return await _ProductBrandRepo.GetByIdAdminDto(routeVal: routeVal, ct: ct);
    }
    public async Task<ProductBrand?> GetByIdAdmin(IdDto routeVal, CancellationToken ct = default)
    {
        return await _ProductBrandRepo.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
    }

    public async Task Add(ProductBrandAdminInputDto input, CancellationToken ct = default)
    {
        var productBrand = new ProductBrandMapper().AdminInputToProductBrand(input);

        if (input.Logo is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Logo);
            productBrand.Logo = uploadRes;
        }

        await _ProductBrandRepo.AddAsync(productBrand, ct);
        await _ProductBrandRepo.SaveChangesAsync(ct);
    }

    public async Task Update(ProductBrand productBrand, ProductBrandAdminInputDto input, CancellationToken ct = default)
    {
        new ProductBrandMapper().AdminInputToProductBrand(input, productBrand);

        if (input.Logo is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Logo);
            productBrand.Logo = uploadRes;
        }

        await _ProductBrandRepo.SaveChangesAsync(ct);
    }

    public async Task Remove(ProductBrand productBrand, CancellationToken ct = default)
    {
        _ProductBrandRepo.Remove(productBrand);
        await _ProductBrandRepo.SaveChangesAsync(ct);
    }
}