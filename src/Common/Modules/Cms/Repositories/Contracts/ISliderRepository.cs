using Common.Commons;

namespace Common.Modules.Cms;

public interface ISliderRepository : IGenericRepository<Slider>
{
    Task<PaginatedList<SliderListDto>> GetSliderLists(SliderListFilterDto filter, CancellationToken ct = default);
    Task<SliderDetailDto?> GetSlider(int id, CancellationToken ct = default);
}