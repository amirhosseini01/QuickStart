using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Cms;

public class SliderRepo : GenericRepository<Slider>, ISliderRepo
{
    private readonly DbSet<Slider> _entities;
    public SliderRepo(BaseDbContext context) : base(context) => _entities = context.Sliders;

    public IQueryable<Slider> FilterQuery(SliderListFilterDto? filter = null)
    {
        var query = _entities.AsNoTracking();

        if (filter is null)
        {
            return query;
        }

        if (filter.Visible is not null)
        {
            query = query.Where(x => x.Visible == filter.Visible.Value);
        }

        return query;
    }
    public IQueryable<Slider> FilterQuery(int id)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return query;
    }
}