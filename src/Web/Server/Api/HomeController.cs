using Common.Commons;
using Common.Modules.Product;
using Microsoft.AspNetCore.Mvc;

namespace Server.Api;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class HomeController : ControllerBase
{
    private readonly ProductService _productService;

    public HomeController(ProductService productService) =>
        _productService = productService;

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(HomeDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get()
    {
        var response = new HomeDataDto()
        {
            ProductsSpecialOffers = await _productService.GetSpecialOffers(),
            ProductsTopDiscount = await _productService.GetTopDiscounts(),
            ProductsTopSales = await _productService.GetTopSales(),
            ProductsSuggested = await _productService.GetSuggested(),
            ProductsTopViewed = await _productService.GetTopViewCount()
        };

        return TypedResults.Ok(response);
    }
}

public class HomeDataDto
{
    public IList<ProductListDto> ProductsSpecialOffers { get; set; }
    public IList<ProductListDto> ProductsSuggested { get; set; }
    public IList<ProductTopSaleListDto> ProductsTopSales { get; set; }
    public IList<ProductListDto> ProductsTopDiscount { get; set; }
    public IList<ProductToViewListDto> ProductsTopViewed { get; set; }
}