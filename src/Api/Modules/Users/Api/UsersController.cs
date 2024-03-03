using Api.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Users;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class UsersController : ControllerBase
{
    // [HttpGet]
    // [ProducesResponseType(typeof(PaginatedList<ProductListDto>), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<IResult> Get([FromQuery] ProductListFilterDto filter, CancellationToken cancellationToken)
    // {
    //     var products = await _productRepository.GetProductLists(filter: filter, cancellationToken: cancellationToken);
    //     return TypedResults.Ok(products);
    // }
}