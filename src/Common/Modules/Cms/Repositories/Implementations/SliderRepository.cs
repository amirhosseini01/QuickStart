using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Cms;

public class SliderRepository : GenericRepository<Slider>, ISliderRepository
{
    private readonly DbSet<Slider> _entities;
    public SliderRepository(ApiDbContext context) : base(context) => _entities = context.Sliders;

    public async Task<PaginatedList<SliderListDto>> GetSliderLists(SliderListFilterDto filter, CancellationToken cancellationToken = default)
    {
        var query = _entities.AsNoTracking();

        return await PaginatedList<SliderListDto>.CreateAsync(source: query.MapSliderToListDto(), filter: filter, cancellationToken: cancellationToken);
    }
    public async Task<SliderDetailDto?> GetSlider(int id, CancellationToken cancellationToken = default)
    {
        var query = _entities.AsNoTracking().Where(x => x.Id == id);
        return await query.MapSliderToDetailDto().FirstOrDefaultAsync(cancellationToken);
    }
}