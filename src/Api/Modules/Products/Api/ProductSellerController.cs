using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductSellerController : ControllerBase
{
    private readonly IProductSellerRepository _ProductSellerRepository;
    private readonly FileUploader _fileUploader;
    public ProductSellerController(IProductSellerRepository ProductSellerRepository, FileUploader fileUploader)
    {
        _ProductSellerRepository = ProductSellerRepository;
        _fileUploader = fileUploader;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductSellerListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductSellerListFilterDto filter, CancellationToken ct)
    {
        var ProductCategories = await _ProductSellerRepository.GetProductSellerList(filter: filter, ct: ct);
        return TypedResults.Ok(ProductCategories);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductSellerDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken ct)
    {
        var ProductSeller = await _ProductSellerRepository.GetProductSeller(id: routeVal.Id, ct: ct);
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
    public async Task<IResult> Post(ProductSellerAdminInputDto input, CancellationToken ct)
    {
        var ProductSeller = new ProductSellerMapper().AdminInputToProductSeller(input);

        if (input.Logo is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Logo);
            ProductSeller.Logo = uploadRes;
        }

        await _ProductSellerRepository.AddAsync(ProductSeller, ct);
        await _ProductSellerRepository.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductSellerAdminInputDto input, CancellationToken ct)
    {
        var ProductSeller = await _ProductSellerRepository.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
        if (ProductSeller is null)
        {
            return TypedResults.NotFound();
        }

        new ProductSellerMapper().AdminInputToProductSeller(input, ProductSeller);

        if (input.Logo is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Logo);
            ProductSeller.Logo = uploadRes;
        }

        await _ProductSellerRepository.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken ct)
    {
        var ProductSeller = await _ProductSellerRepository.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
        if (ProductSeller is null)
        {
            return TypedResults.NotFound();
        }

        _ProductSellerRepository.Remove(ProductSeller);
        await _ProductSellerRepository.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }
}

