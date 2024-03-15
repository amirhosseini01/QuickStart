using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductModelController : ControllerBase
{
    private readonly IProductModelRepository _ProductModelRepository;
    public ProductModelController(IProductModelRepository ProductModelRepository) =>
        _ProductModelRepository = ProductModelRepository;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductModelListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductModelListFilterDto filter, CancellationToken cancellationToken)
    {
        var productModels = await _ProductModelRepository.GetProductModelList(filter: filter, cancellationToken: cancellationToken);
        return TypedResults.Ok(productModels);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductModelDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> CommonGet(IdDto routeVal, CancellationToken cancellationToken)
    {
        var productModel = await _ProductModelRepository.GetProductModel(id: routeVal.Id, cancellationToken: cancellationToken);
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
    public async Task<IResult> Post(ProductModelAdminInputDto input, CancellationToken cancellationToken)
    {
        var productModel = new ProductModelMapper().AdminInputToProductModel(input);

        await _ProductModelRepository.AddAsync(productModel, cancellationToken);
        await _ProductModelRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductModelAdminInputDto input, CancellationToken cancellationToken)
    {
        var productModel = await _ProductModelRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (productModel is null)
        {
            return TypedResults.NotFound();
        }

        new ProductModelMapper().AdminInputToProductModel(input, productModel);

        await _ProductModelRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken cancellationToken)
    {
        var productModel = await _ProductModelRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (productModel is null)
        {
            return TypedResults.NotFound();
        }

        _ProductModelRepository.Remove(productModel);
        await _ProductModelRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }
}

