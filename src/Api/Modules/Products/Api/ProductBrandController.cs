using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductBrandController : ControllerBase
{
    private readonly IProductBrandRepository _ProductBrandRepository;
    private readonly FileUploader _fileUploader;
    public ProductBrandController(IProductBrandRepository ProductBrandRepository, FileUploader fileUploader)
    {
        _ProductBrandRepository = ProductBrandRepository;
        _fileUploader = fileUploader;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductBrandListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductBrandListFilterDto filter, CancellationToken cancellationToken)
    {
        var ProductCategories = await _ProductBrandRepository.GetProductBrandList(filter: filter, cancellationToken: cancellationToken);
        return TypedResults.Ok(ProductCategories);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductBrandDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken cancellationToken)
    {
        var ProductBrand = await _ProductBrandRepository.GetProductBrand(id: routeVal.Id, cancellationToken: cancellationToken);
        if (ProductBrand is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(ProductBrand);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Post(ProductBrandAdminInputDto input, CancellationToken cancellationToken)
    {
        var productBrand = new ProductBrandMapper().AdminInputToProductBrand(input);

        if (input.Logo is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Logo);
            productBrand.Logo = uploadRes;
        }

        await _ProductBrandRepository.AddAsync(productBrand, cancellationToken);
        await _ProductBrandRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductBrandAdminInputDto input, CancellationToken cancellationToken)
    {
        var productBrand = await _ProductBrandRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (productBrand is null)
        {
            return TypedResults.NotFound();
        }

        new ProductBrandMapper().AdminInputToProductBrand(input, productBrand);

        if (input.Logo is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Logo);
            productBrand.Logo = uploadRes;
        }

        await _ProductBrandRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken cancellationToken)
    {
        var ProductBrand = await _ProductBrandRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (ProductBrand is null)
        {
            return TypedResults.NotFound();
        }

        _ProductBrandRepository.Remove(ProductBrand);
        await _ProductBrandRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }
}

