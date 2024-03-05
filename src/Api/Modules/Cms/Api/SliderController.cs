using Api.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Cms;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class SliderController : ControllerBase
{
    private readonly ISliderRepository _sliderRepository;
    private readonly FileUploader _fileUploader;
    public SliderController(ISliderRepository sliderRepository, FileUploader fileUploader)
    {
        _sliderRepository = sliderRepository;
        _fileUploader = fileUploader;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<SliderListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] SliderListFilterDto filter, CancellationToken cancellationToken)
    {
        var sliders = await _sliderRepository.GetSliderLists(filter: filter, cancellationToken: cancellationToken);
        return TypedResults.Ok(sliders);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(SliderDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.GetSlider(id: routeVal.Id, cancellationToken: cancellationToken);
        if (slider is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(slider);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Post(SliderAdminInputDto input, CancellationToken cancellationToken)
    {
        var slider = new SliderMapper().AdminInputToSlider(input);

        var imageUploadRes = await _fileUploader.UploadFile(input.Image);
        slider.Image = imageUploadRes;

        var thumbnailUploadRes = await _fileUploader.UploadFile(input.Thumbnail);
        slider.Thumbnail = thumbnailUploadRes;

        await _sliderRepository.AddAsync(slider, cancellationToken);
        await _sliderRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, SliderAdminInputEditDto input, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (slider is null)
        {
            return TypedResults.NotFound();
        }

        if (input.Image is not null)
        {
            var imageUploadRes = await _fileUploader.UploadFile(input.Image);
            slider.Image = imageUploadRes;
        }

        if (input.Thumbnail is not null)
        {
            var thumbnailUploadRes = await _fileUploader.UploadFile(input.Thumbnail);
            slider.Thumbnail = thumbnailUploadRes;
        }
        
        new SliderMapper().AdminInputToSlider(input, slider);
        await _sliderRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken cancellationToken)
    {
        var slider = await _sliderRepository.FirstOrDefaultAsync(id: routeVal.Id, cancellationToken: cancellationToken);
        if (slider is null)
        {
            return TypedResults.NotFound();
        }

        _sliderRepository.Remove(slider);
        await _sliderRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }
}

