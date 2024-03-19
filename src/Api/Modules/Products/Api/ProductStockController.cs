using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductStockController : ControllerBase
{
    private readonly IProductStockRepository _ProductStockRepository;
    public ProductStockController(IProductStockRepository ProductStockRepository) =>
        _ProductStockRepository = ProductStockRepository;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductStockListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductStockListFilterDto filter, CancellationToken ct)
    {
        var productStocks = await _ProductStockRepository.GetProductStockList(filter: filter, ct: ct);
        return TypedResults.Ok(productStocks);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductStockDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken ct)
    {
        var productStock = await _ProductStockRepository.GetProductStock(id: routeVal.Id, ct: ct);
        if (productStock is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(productStock);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Post(ProductStockAdminInputDto input, CancellationToken ct)
    {
        var productStock = new ProductStockMapper().AdminInputToProductStock(input);

        await _ProductStockRepository.AddAsync(productStock, ct);
        await _ProductStockRepository.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductStockAdminInputDto input, CancellationToken ct)
    {
        var productStock = await _ProductStockRepository.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
        if (productStock is null)
        {
            return TypedResults.NotFound();
        }

        new ProductStockMapper().AdminInputToProductStock(input, productStock);

        await _ProductStockRepository.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken ct)
    {
        var productStock = await _ProductStockRepository.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
        if (productStock is null)
        {
            return TypedResults.NotFound();
        }

        _ProductStockRepository.Remove(productStock);
        await _ProductStockRepository.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }
}

