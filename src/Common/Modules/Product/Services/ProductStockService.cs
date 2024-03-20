using Common.Commons;

namespace Common.Modules.Product;

public class ProductStockService
{
    private readonly IProductStockRepo _productStockRepo;
    public ProductStockService(IProductStockRepo productStockRepository)
    {
        _productStockRepo = productStockRepository;
    }

    public async Task<PaginatedList<ProductStockListDto>> GetAdminListDto(ProductStockListFilterDto filter, CancellationToken ct = default)
    {
        return await _productStockRepo.GetAdminListDto(filter: filter, ct: ct);
    }

    public async Task<ProductStockDetailDto?> GetByIdAdminDto(IdDto routeVal, CancellationToken ct = default)
    {
        return await _productStockRepo.GetByIdAdminDto(routeVal: routeVal, ct: ct);
    }

    public async Task<ProductStock?> GetByIdAdmin(IdDto routeVal, CancellationToken ct = default)
    {
        return await _productStockRepo.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
    }

    public async Task Add(ProductStockAdminInputDto input, CancellationToken ct = default)
    {
        var productStock = new ProductStockMapper().AdminInputToProductStock(input);

        await _productStockRepo.AddAsync(productStock, ct);
        await _productStockRepo.SaveChangesAsync(ct);
    }

    public async Task Update(ProductStock productStock, ProductStockAdminInputDto input, CancellationToken ct = default)
    {
        new ProductStockMapper().AdminInputToProductStock(input, productStock);

        await _productStockRepo.SaveChangesAsync(ct);
    }

    public async Task Remove(ProductStock productStock, CancellationToken ct = default)
    {
        _productStockRepo.Remove(productStock);
        await _productStockRepo.SaveChangesAsync(ct);
    }
}