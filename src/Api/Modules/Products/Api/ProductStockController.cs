using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductStockController : ControllerBase
{
    private readonly ProductStockService _productStockService;
    public ProductStockController(ProductStockService productStockService) =>
        _productStockService = productStockService;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductStockListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductStockListFilterDto filter, CancellationToken ct = default)
    {
        var productStocks = await _productStockService.GetAdminListDto(filter: filter, ct: ct);
        return TypedResults.Ok(productStocks);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductStockDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken ct = default)
    {
        var productStock = await _productStockService.GetByIdAdminDto(routeVal: routeVal, ct: ct);
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
    public async Task<IResult> Post(ProductStockAdminInputDto input, CancellationToken ct = default)
    {
        await _productStockService.Add(input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductStockAdminInputDto input, CancellationToken ct = default)
    {
        var productStock = await _productStockService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (productStock is null)
        {
            return TypedResults.NotFound();
        }

        await _productStockService.Update(productStock: productStock, input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Remove(IdDto routeVal, CancellationToken ct = default)
    {
        var productStock = await _productStockService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (productStock is null)
        {
            return TypedResults.NotFound();
        }

        await _productStockService.Remove(productStock: productStock, ct: ct);

        return TypedResults.Ok();
    }
}

