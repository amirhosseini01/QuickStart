using Common.Commons;

namespace Common.Modules.Cms;

public class SliderService : IGenericService
{
    private readonly ISliderRepo _sliderRepo;
    private readonly FileUploader _fileUploader;
    public SliderService(ISliderRepo sliderRepo, FileUploader fileUploader)
    {
        _sliderRepo = sliderRepo;
        _fileUploader = fileUploader;
    }

    public async Task<PaginatedList<SliderListDto>> GetAdminListDto(SliderListFilterDto filter, CancellationToken ct = default)
    {
        return await _sliderRepo.GetAdminListDto(filter: filter, ct: ct);
    }

    public async Task<SliderDetailDto?> GetByIdAdminDto(IdDto routeVal, CancellationToken ct = default)
    {
        return await _sliderRepo.GetByIdAdminDto(routeVal: routeVal, ct: ct);
    }

    public async Task<Slider?> GetByIdAdmin(IdDto routeVal, CancellationToken ct = default)
    {
        return await _sliderRepo.FirstOrDefaultAsync(id: routeVal.Id, ct: ct);
    }

    public async Task Add(SliderAdminInputDto input, CancellationToken ct = default)
    {
        var slider = new SliderMapper().AdminInputToSlider(input);

        var imageUploadRes = await _fileUploader.UploadFile(input.Image);
        slider.Image = imageUploadRes;

        var thumbnailUploadRes = await _fileUploader.UploadFile(input.Thumbnail);
        slider.Thumbnail = thumbnailUploadRes;

        await _sliderRepo.AddAsync(slider, ct);
        await _sliderRepo.SaveChangesAsync(ct);
    }

    public async Task Update(Slider slider, SliderAdminInputEditDto input, CancellationToken ct = default)
    {
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
        await _sliderRepo.SaveChangesAsync(ct);
    }

    public async Task Remove(Slider slider, CancellationToken ct = default)
    {
        _sliderRepo.Remove(slider);
        await _sliderRepo.SaveChangesAsync(ct);
    }
}