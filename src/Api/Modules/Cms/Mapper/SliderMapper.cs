using Riok.Mapperly.Abstractions;

namespace Api.Modules.Cms;

[Mapper]
public static partial class SliderMapperQuery
{
    public static partial IQueryable<SliderDetailDto> MapSliderToDetailDto(this IQueryable<Slider> q);
    public static partial IQueryable<SliderListDto> MapSliderToListDto(this IQueryable<Slider> q);
}

[Mapper]
public partial class SliderMapper
{
    [MapperIgnoreSource(nameof(Slider.Image))]
    [MapperIgnoreSource(nameof(Slider.Thumbnail))]
    public partial Slider AdminInputToSlider(SliderAdminInputDto input);
    
    [MapperIgnoreSource(nameof(Slider.Image))]
    [MapperIgnoreSource(nameof(Slider.Thumbnail))]
    public partial void AdminInputToSlider(SliderAdminInputDto input, Slider Slider);
}