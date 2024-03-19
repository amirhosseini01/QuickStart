using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductCategoryController : ControllerBase
{
    private readonly ProductCategoryServices _productCategoryServices;
    public ProductCategoryController(ProductCategoryServices productCategoryServices) =>
        _productCategoryServices = productCategoryServices;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductCategoryListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> GetList([FromQuery] ProductCategoryListFilterDto filter, CancellationToken cancellationToken = default)
    {
        var entities = await _productCategoryServices.GetAdminList(filter, cancellationToken);
        return TypedResults.Ok(entities);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductCategoryDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> GetById(IdDto routeVal, CancellationToken cancellationToken = default)
    {
        var entity = await _productCategoryServices.GetByIdDto(routeVal: routeVal, cancellationToken: cancellationToken);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(entity);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Post(ProductCategoryAdminInputDto input, CancellationToken cancellationToken = default)
    {
        var entity = await _productCategoryServices.Add(input, cancellationToken);

        return TypedResults.Ok(entity);
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductCategoryAdminInputDto input, CancellationToken cancellationToken = default)
    {
        var entity = await _productCategoryServices.GetById(routeVal, cancellationToken);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        await _productCategoryServices.Update(input, entity, cancellationToken);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Remove(IdDto routeVal, CancellationToken cancellationToken = default)
    {
        var entity = await _productCategoryServices.GetById(routeVal, cancellationToken);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        await _productCategoryServices.Remove(entity, cancellationToken);

        return TypedResults.Ok();
    }
}

