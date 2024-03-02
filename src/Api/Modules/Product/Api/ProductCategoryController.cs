using Api.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductCategoryController : ControllerBase
{
    private readonly IProductCategoryRepository _ProductCategoryRepository;
    public ProductCategoryController(IProductCategoryRepository ProductCategoryRepository) => _ProductCategoryRepository = ProductCategoryRepository;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductCategoryListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductCategoryListFilterDto filter, CancellationToken cancellationToken)
    {
        var ProductCategories = await _ProductCategoryRepository.GetProductCategoryList(filter: filter, cancellationToken: cancellationToken);
        return TypedResults.Ok(ProductCategories);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductCategoryDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromRoute] IdDto id, CancellationToken cancellationToken)
    {
        var ProductCategory = await _ProductCategoryRepository.GetProductCategory(id: id.Id, cancellationToken: cancellationToken);
        if (ProductCategory is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(ProductCategory);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Post(ProductCategoryAdminInputDto input, CancellationToken cancellationToken)
    {
        var ProductCategory = new ProductCategoryMapper().AdminInputToProductCategory(input);

        await _ProductCategoryRepository.AddAsync(ProductCategory, cancellationToken);
        await _ProductCategoryRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put([FromRoute] IdDto id, ProductCategoryAdminInputDto input, CancellationToken cancellationToken)
    {
        var ProductCategory = await _ProductCategoryRepository.FirstOrDefaultAsync(id: id.Id, cancellationToken: cancellationToken);
        if (ProductCategory is null)
        {
            return TypedResults.NotFound();
        }

        new ProductCategoryMapper().AdminInputToProductCategory(ProductCategory, input);

        await _ProductCategoryRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete([FromRoute] IdDto id, CancellationToken cancellationToken)
    {
        var ProductCategory = await _ProductCategoryRepository.FirstOrDefaultAsync(id: id.Id, cancellationToken: cancellationToken);
        if (ProductCategory is null)
        {
            return TypedResults.NotFound();
        }

        _ProductCategoryRepository.Remove(ProductCategory);
        await _ProductCategoryRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }
}

