using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductCategoryController : ControllerBase
{
    private readonly ProductCategoryService _productCategoryServices;
    public ProductCategoryController(ProductCategoryService productCategoryServices) =>
        _productCategoryServices = productCategoryServices;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductCategoryAdminListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> GetList([FromQuery] ProductCategoryListFilterDto filter, CancellationToken ct = default)
    {
        var entities = await _productCategoryServices.GetAdminList(filter, ct);
        return TypedResults.Ok(entities);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductCategoryAdminDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> GetById(IdDto routeVal, CancellationToken ct = default)
    {
        var entity = await _productCategoryServices.GetByIdAdminDto(routeVal: routeVal, ct: ct);
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
    public async Task<IResult> Post(ProductCategoryAdminInputDto input, CancellationToken ct = default)
    {
        var entity = await _productCategoryServices.Add(input, ct);

        return TypedResults.Ok(entity);
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductCategoryAdminInputDto input, CancellationToken ct = default)
    {
        var entity = await _productCategoryServices.GetByIdAdmin(routeVal, ct);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        await _productCategoryServices.Update(input, entity, ct);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Remove(IdDto routeVal, CancellationToken ct = default)
    {
        var entity = await _productCategoryServices.GetByIdAdmin(routeVal, ct);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        await _productCategoryServices.Remove(entity, ct);

        return TypedResults.Ok();
    }
}

