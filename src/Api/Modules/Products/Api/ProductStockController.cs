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
    public async Task<IResult> Get([FromQuery] ProductStockListFilterDto filter, CancellationToken cancellationToken)
    {
        var productStocks = await _ProductStockRepository.GetProductStockList(filter: filter, cancellationToken: cancellationToken);
        return TypedResults.Ok(productStocks);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductStockDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken cancellationToken)
    {
        var productStock = await _ProductStockRepository.GetProductStock(id: routeVal.Id, cancellationToken: cancellationToken);
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
    public async Task<IResult> Post(ProductStockAdminInputDto input, CancellationToken cancellationToken)
    {
        var productStock = new ProductStockMapper().AdminInputToProductStock(input);

        await _ProductStockRepository.AddAsync(productStock, cancellationToken);
        await _ProductStockRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductStockAdminInputDto input, CancellationToken cancellationToken)
    {
        var productStock = await _ProductStockRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (productStock is null)
        {
            return TypedResults.NotFound();
        }

        new ProductStockMapper().AdminInputToProductStock(input, productStock);

        await _ProductStockRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken cancellationToken)
    {
        var productStock = await _ProductStockRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (productStock is null)
        {
            return TypedResults.NotFound();
        }

        _ProductStockRepository.Remove(productStock);
        await _ProductStockRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }
}

