using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductModelController : ControllerBase
{
    private readonly ProductModelService _productModelService;
    public ProductModelController(ProductModelService productModelService) =>
        _productModelService = productModelService;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductModelListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductModelListFilterDto filter, CancellationToken ct = default)
    {
        var productModels = await _productModelService.GetAdminListDto(filter: filter, ct: ct);
        return TypedResults.Ok(productModels);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductModelDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken ct = default)
    {
        var productModel = await _productModelService.GetByIdAdminDto(routeVal: routeVal, ct: ct);
        if (productModel is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(productModel);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Post(ProductModelAdminInputDto input, CancellationToken ct = default)
    {
        await _productModelService.Add(input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductModelAdminInputDto input, CancellationToken ct = default)
    {
        var productModel = await _productModelService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (productModel is null)
        {
            return TypedResults.NotFound();
        }

        await _productModelService.Update(productModel: productModel, input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken ct = default)
    {
        var productModel = await _productModelService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (productModel is null)
        {
            return TypedResults.NotFound();
        }

        await _productModelService.Remove(productModel: productModel);

        return TypedResults.Ok();
    }
}

