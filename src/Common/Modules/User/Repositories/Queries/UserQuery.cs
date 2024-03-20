using Common.Commons;


namespace Common.Modules.User;

public static class UserQuery
{
    public static async Task<PaginatedList<UserListDto>> GetAdminList(this IUserRepo userRepo, UserListFilterDto filter, CancellationToken ct = default)
    {
        var query = userRepo.FilterQuery(filter: filter).OrderByDescending(x => x.Id);

        return await PaginatedList<UserListDto>.CreateAsync(
            source: query.MapUserToListDto(),
            filter: filter,
            ct: ct);
    }
}