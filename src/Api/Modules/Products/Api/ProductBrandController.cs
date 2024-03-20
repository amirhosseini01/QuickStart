using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductBrandController : ControllerBase
{
    private readonly ProductBrandService _productBrandService;

    public ProductBrandController(ProductBrandService productBrandService) =>
        _productBrandService = productBrandService;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductBrandListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductBrandListFilterDto filter, CancellationToken ct = default)
    {
        var ProductCategories = await _productBrandService.GetAdminList(filter: filter, ct: ct);
        return TypedResults.Ok(ProductCategories);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductBrandDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken ct = default)
    {
        var ProductBrand = await _productBrandService.GetByIdAdminDto(routeVal: routeVal, ct: ct);
        if (ProductBrand is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(ProductBrand);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Post(ProductBrandAdminInputDto input, CancellationToken ct = default)
    {
        await _productBrandService.Add(input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductBrandAdminInputDto input, CancellationToken ct = default)
    {
        var productBrand = await _productBrandService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (productBrand is null)
        {
            return TypedResults.NotFound();
        }

        await _productBrandService.Update(productBrand: productBrand, input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Remove(IdDto routeVal, CancellationToken ct = default)
    {
        var productBrand = await _productBrandService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (productBrand is null)
        {
            return TypedResults.NotFound();
        }

        await _productBrandService.Remove(productBrand: productBrand, ct: ct);

        return TypedResults.Ok();
    }
}

