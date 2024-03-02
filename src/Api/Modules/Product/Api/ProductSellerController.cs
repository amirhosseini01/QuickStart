using Api.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductSellerController : ControllerBase
{
    private readonly IProductSellerRepository _ProductSellerRepository;
    public ProductSellerController(IProductSellerRepository ProductSellerRepository) => _ProductSellerRepository = ProductSellerRepository;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductSellerListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductSellerListFilterDto filter, CancellationToken cancellationToken)
    {
        var ProductCategories = await _ProductSellerRepository.GetProductSellerList(filter: filter, cancellationToken: cancellationToken);
        return TypedResults.Ok(ProductCategories);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductSellerDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken cancellationToken)
    {
        var ProductSeller = await _ProductSellerRepository.GetProductSeller(id: routeVal.Id, cancellationToken: cancellationToken);
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
    public async Task<IResult> Post(ProductSellerAdminInputDto input, CancellationToken cancellationToken)
    {
        var ProductSeller = new ProductSellerMapper().AdminInputToProductSeller(input);

        await _ProductSellerRepository.AddAsync(ProductSeller, cancellationToken);
        await _ProductSellerRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductSellerAdminInputDto input, CancellationToken cancellationToken)
    {
        var ProductSeller = await _ProductSellerRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (ProductSeller is null)
        {
            return TypedResults.NotFound();
        }

        new ProductSellerMapper().AdminInputToProductSeller(ProductSeller, input);

        await _ProductSellerRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken cancellationToken)
    {
        var ProductSeller = await _ProductSellerRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (ProductSeller is null)
        {
            return TypedResults.NotFound();
        }

        _ProductSellerRepository.Remove(ProductSeller);
        await _ProductSellerRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }
}

