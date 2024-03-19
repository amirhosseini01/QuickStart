using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly FileUploader _fileUploader;
    public ProductController(IProductRepository productRepository, FileUploader fileUploader)
    {
        _productRepository = productRepository;
        _fileUploader = fileUploader;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductListFilterDto filter, CancellationToken ct)
    {
        var products = await _productRepository.GetProductLists(filter: filter, ct: ct);
        return TypedResults.Ok(products);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken ct)
    {
        var product = await _productRepository.GetProduct(id: routeVal.Id, ct: ct);
        if (product is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Post(ProductAdminInputDto input, CancellationToken ct)
    {
        var product = new ProductMapper().AdminInputToProduct(input);

        var imageUploadRes = await _fileUploader.UploadFile(input.Image);
        product.Image = imageUploadRes;

        var thumbnailUploadRes = await _fileUploader.UploadFile(input.Thumbnail);
        product.Thumbnail = thumbnailUploadRes;

        await _productRepository.AddAsync(product, ct);
        await _productRepository.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductAdminInputEditDto input, CancellationToken ct)
    {
        var product = await _productRepository.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
        if (product is null)
        {
            return TypedResults.NotFound();
        }

        if (input.Image is not null)
        {
            var imageUploadRes = await _fileUploader.UploadFile(input.Image);
            product.Image = imageUploadRes;
        }

        if (input.Thumbnail is not null)
        {
            var thumbnailUploadRes = await _fileUploader.UploadFile(input.Thumbnail);
            product.Thumbnail = thumbnailUploadRes;
        }

        new ProductMapper().AdminInputToProduct(input, product);
        await _productRepository.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken ct)
    {
        var product = await _productRepository.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
        if (product is null)
        {
            return TypedResults.NotFound();
        }

        _productRepository.Remove(product);
        await _productRepository.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }
}

