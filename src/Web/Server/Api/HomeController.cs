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
    private readonly ProductCategoryService _productCategoryService;

    public HomeController(ProductService productService,
        ProductCategoryService productCategoryService)
    {
        _productService = productService;
        this._productCategoryService = productCategoryService;
    }

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
            ProductsTopViewed = await _productService.GetTopViewCount(),


            ProductCategories = await _productCategoryService.GetList()
        };

        return TypedResults.Ok(response);
    }
}

public class HomeDataDto
{
    public PaginatedList<ProductListDto> ProductsSpecialOffers { get; set; }
    public PaginatedList<ProductListDto> ProductsSuggested { get; set; }
    public PaginatedList<ProductTopSaleListDto> ProductsTopSales { get; set; }
    public PaginatedList<ProductListDto> ProductsTopDiscount { get; set; }
    public PaginatedList<ProductToViewListDto> ProductsTopViewed { get; set; }
    public PaginatedList<ProductCategoryListDto> ProductCategories { get; set; }
}