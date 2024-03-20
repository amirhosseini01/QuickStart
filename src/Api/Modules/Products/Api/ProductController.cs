using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService) =>
        _productService = productService;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductListFilterDto filter, CancellationToken ct = default)
    {
        var products = await _productService.GetAdminListDto(filter: filter, ct: ct);
        return TypedResults.Ok(products);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken ct = default)
    {
        var product = await _productService.GetByIdAdminDto(routeVal: routeVal, ct: ct);
        if (product is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Post(ProductAdminInputDto input, CancellationToken ct = default)
    {
        await _productService.Add(input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductAdminInputEditDto input, CancellationToken ct = default)
    {
        var product = await _productService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (product is null)
        {
            return TypedResults.NotFound();
        }

        await _productService.Update(product: product, input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Remove(IdDto routeVal, CancellationToken ct = default)
    {
        var product = await _productService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (product is null)
        {
            return TypedResults.NotFound();
        }

        await _productService.Remove(product);

        return TypedResults.Ok();
    }
}

