using Common.Commons;

namespace Common.Modules.Product;

public class ProductModelService : IGenericService
{
    private readonly IProductModelRepo _productModelRepo;
    public ProductModelService(IProductModelRepo productModelRepository)
    {
        _productModelRepo = productModelRepository;
    }

    public async Task<PaginatedList<ProductModelListDto>> GetAdminListDto(ProductModelListFilterDto filter, CancellationToken ct = default)
    {
        return await _productModelRepo.GetAdminListDto(filter: filter, ct: ct);
    }

    public async Task<ProductModelDetailDto?> GetByIdAdminDto(IdDto routeVal, CancellationToken ct = default)
    {
        return await _productModelRepo.GetByIdAdminDto(routeVal: routeVal, ct: ct);
    }

    public async Task<ProductModel?> GetByIdAdmin(IdDto routeVal, CancellationToken ct = default)
    {
        return await _productModelRepo.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
    }

    public async Task Add(ProductModelAdminInputDto input, CancellationToken ct = default)
    {
        var productModel = new ProductModelMapper().AdminInputToProductModel(input);

        await _productModelRepo.AddAsync(productModel, ct);
        await _productModelRepo.SaveChangesAsync(ct);
    }

    public async Task Update(ProductModel productModel, ProductModelAdminInputDto input, CancellationToken ct = default)
    {
        new ProductModelMapper().AdminInputToProductModel(input, productModel);

        await _productModelRepo.SaveChangesAsync(ct);
    }

    public async Task Remove(ProductModel productModel, CancellationToken ct = default)
    {
        _productModelRepo.Remove(productModel);
        await _productModelRepo.SaveChangesAsync(ct);
    }
}