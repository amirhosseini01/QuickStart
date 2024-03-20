using Common.Commons;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.Cms;

public static class SliderQuery
{
    public static async Task<List<SliderListDto>> GetVisibleSliders(this ISliderRepo sliderRepo, CancellationToken ct = default)
    {
        return await sliderRepo.FilterQuery().MapSliderToListDto().ToListAsync(ct);
    }

    public static async Task<PaginatedList<SliderListDto>> GetAdminListDto(this ISliderRepo sliderRepo, SliderListFilterDto filter, CancellationToken ct = default)
    {
        var query = sliderRepo.FilterQuery(filter: filter).OrderByDescending(x => x.Id);

        return await PaginatedList<SliderListDto>.CreateAsync(
            source: query.MapSliderToListDto(),
            filter: filter,
            ct: ct);
    }

    public static async Task<SliderDetailDto?> GetByIdAdminDto(this ISliderRepo sliderRepo, IdDto routeVal, CancellationToken ct = default)
    {
        var query = sliderRepo.FilterQuery(routeVal.Id);
        return await query.MapSliderToDetailDto().FirstOrDefaultAsync(ct);
    }
}