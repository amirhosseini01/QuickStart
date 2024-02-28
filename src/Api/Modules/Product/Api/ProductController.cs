using Api.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Product;

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
    [ProducesResponseType(typeof(PaginatedList<ProductListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductListFilterDto filter, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductLists(filter: filter, cancellationToken: cancellationToken);
        return TypedResults.Ok(products);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromRoute] IdDto id, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProduct(id: id.Id, cancellationToken: cancellationToken);
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
    public async Task<IResult> Post(ProductAdminInputDto input, CancellationToken cancellationToken)
    {
        var product = new ProductMapper().AdminInputToProduct(input);

        var imageUploadRes = await _fileUploader.UploadFile(input.Image);
        product.Image = imageUploadRes;

        var thumbnailUploadRes = await _fileUploader.UploadFile(input.Thumbnail);
        product.Thumbnail = thumbnailUploadRes;

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put([FromRoute] IdDto id, ProductAdminInputEditDto input, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FirstOrDefaultAsync(id: id.Id, cancellationToken: cancellationToken);
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

        new ProductMapper().AdminInputToProduct(product, input);

        await _productRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete([FromRoute] IdDto id, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FirstOrDefaultAsync(id: id.Id, cancellationToken: cancellationToken);
        if (product is null)
        {
            return TypedResults.NotFound();
        }

        _productRepository.Remove(product);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }
}

