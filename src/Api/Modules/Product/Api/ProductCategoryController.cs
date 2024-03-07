using Api.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Product;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class ProductCategoryController : ControllerBase
{
    private readonly IProductCategoryRepository _ProductCategoryRepository;
    private readonly FileUploader _fileUploader;
	public ProductCategoryController(IProductCategoryRepository ProductCategoryRepository, FileUploader fileUploader)
	{
		_ProductCategoryRepository = ProductCategoryRepository;
		_fileUploader = fileUploader;
	}

	[HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ProductCategoryListDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] ProductCategoryListFilterDto filter, CancellationToken cancellationToken)
    {
        var ProductCategories = await _ProductCategoryRepository.GetProductCategoryList(filter: filter, cancellationToken: cancellationToken);
        return TypedResults.Ok(ProductCategories);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ProductCategoryDetailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken cancellationToken)
    {
        var ProductCategory = await _ProductCategoryRepository.GetProductCategory(id: routeVal.Id, cancellationToken: cancellationToken);
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

        if (input.Image is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Image);
            ProductCategory.Image = uploadRes;
        }

        await _ProductCategoryRepository.AddAsync(ProductCategory, cancellationToken);
        await _ProductCategoryRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, ProductCategoryAdminInputDto input, CancellationToken cancellationToken)
    {
        var ProductCategory = await _ProductCategoryRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (ProductCategory is null)
        {
            return TypedResults.NotFound();
        }

        new ProductCategoryMapper().AdminInputToProductCategory(input, ProductCategory);

        if (input.Image is not null)
        {
            var uploadRes = await _fileUploader.UploadFile(input.Image);
            ProductCategory.Image = uploadRes;
        }

        await _ProductCategoryRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken cancellationToken)
    {
        var ProductCategory = await _ProductCategoryRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (ProductCategory is null)
        {
            return TypedResults.NotFound();
        }

        _ProductCategoryRepository.Remove(ProductCategory);
        await _ProductCategoryRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }
}

