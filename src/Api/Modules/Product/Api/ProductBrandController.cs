using Api.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductBrandController : ControllerBase
{
    private readonly IProductBrandRepository _ProductBrandRepository;
    public ProductBrandController(IProductBrandRepository ProductBrandRepository) => _ProductBrandRepository = ProductBrandRepository;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductBrandListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductBrandListFilterDto filter, CancellationToken cancellationToken)
    {
        var ProductCategories = await _ProductBrandRepository.GetProductBrandList(filter: filter, cancellationToken: cancellationToken);
        return TypedResults.Ok(ProductCategories);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductBrandDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken cancellationToken)
    {
        var ProductBrand = await _ProductBrandRepository.GetProductBrand(id: routeVal.Id, cancellationToken: cancellationToken);
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
    public async Task<IResult> Post(ProductBrandAdminInputDto input, CancellationToken cancellationToken)
    {
        var ProductBrand = new ProductBrandMapper().AdminInputToProductBrand(input);

        await _ProductBrandRepository.AddAsync(ProductBrand, cancellationToken);
        await _ProductBrandRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductBrandAdminInputDto input, CancellationToken cancellationToken)
    {
        var ProductBrand = await _ProductBrandRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (ProductBrand is null)
        {
            return TypedResults.NotFound();
        }

        new ProductBrandMapper().AdminInputToProductBrand(input, ProductBrand);

        await _ProductBrandRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken cancellationToken)
    {
        var ProductBrand = await _ProductBrandRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (ProductBrand is null)
        {
            return TypedResults.NotFound();
        }

        _ProductBrandRepository.Remove(ProductBrand);
        await _ProductBrandRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }
}

