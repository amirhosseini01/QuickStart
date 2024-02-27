using Api.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    public ProductController(IProductRepository productRepository) => _productRepository = productRepository;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Products([FromQuery] ProductListFilterDto filter, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductLists(filter: filter, cancellationToken: cancellationToken);
        return TypedResults.Ok(products);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Product([FromRoute] int id, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProduct(id: id, cancellationToken: cancellationToken);
        if (product is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(product);
    }
}

