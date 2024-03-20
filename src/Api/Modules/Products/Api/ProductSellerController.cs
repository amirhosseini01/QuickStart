using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductSellerController : ControllerBase
{
    private readonly ProductSellerService _productSellerService;

    public ProductSellerController(ProductSellerService productSellerService) =>
        _productSellerService = productSellerService;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductSellerListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductSellerListFilterDto filter, CancellationToken ct = default)
    {
        var ProductCategories = await _productSellerService.GetAdminListDto(filter: filter, ct: ct);
        return TypedResults.Ok(ProductCategories);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductSellerDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken ct = default)
    {
        var ProductSeller = await _productSellerService.GetByIdAdminDto(routeVal: routeVal, ct: ct);
        if (ProductSeller is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(ProductSeller);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Post(ProductSellerAdminInputDto input, CancellationToken ct = default)
    {
        await _productSellerService.Add(input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductSellerAdminInputDto input, CancellationToken ct = default)
    {
        var productSeller = await _productSellerService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (productSeller is null)
        {
            return TypedResults.NotFound();
        }

        await _productSellerService.Update(productSeller: productSeller, input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken ct = default)
    {
        var ProductSeller = await _productSellerService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (ProductSeller is null)
        {
            return TypedResults.NotFound();
        }

        await _productSellerService.Remove(ProductSeller);

        return TypedResults.Ok();
    }
}

