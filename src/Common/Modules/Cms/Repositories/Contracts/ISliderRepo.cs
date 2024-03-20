using Common.Commons;

namespace Common.Modules.Cms;

public interface ISliderRepo : IGenericRepository<Slider>
{
    IQueryable<Slider> FilterQuery(SliderListFilterDto? filter = null);
    IQueryable<Slider> FilterQuery(int id);
}