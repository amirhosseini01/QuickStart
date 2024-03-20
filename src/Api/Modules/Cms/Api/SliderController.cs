using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.Cms;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class SliderController : ControllerBase
{
    private readonly SliderService _sliderService;
    public SliderController(SliderService sliderService) =>
        _sliderService = sliderService;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<SliderListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] SliderListFilterDto filter, CancellationToken ct)
    {
        var sliders = await _sliderService.GetAdminListDto(filter: filter, ct: ct);
        return TypedResults.Ok(sliders);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(SliderDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(IdDto routeVal, CancellationToken ct)
    {
        var slider = await _sliderService.GetByIdAdminDto(routeVal: routeVal, ct: ct);
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
    public async Task<IResult> Post(SliderAdminInputDto input, CancellationToken ct)
    {
        await _sliderService.Add(input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Put(IdDto routeVal, SliderAdminInputEditDto input, CancellationToken ct)
    {
        var slider = await _sliderService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (slider is null)
        {
            return TypedResults.NotFound();
        }

        await _sliderService.Update(slider: slider, input: input, ct: ct);

        return TypedResults.Ok();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IResult> Delete(IdDto routeVal, CancellationToken ct)
    {
        var slider = await _sliderService.GetByIdAdmin(routeVal: routeVal, ct: ct);
        if (slider is null)
        {
            return TypedResults.NotFound();
        }

        await _sliderService.Remove(slider);

        return TypedResults.Ok();
    }
}

