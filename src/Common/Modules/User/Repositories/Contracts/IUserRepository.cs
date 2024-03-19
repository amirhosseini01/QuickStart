using Common.Commons;
using Common.Modules.User;

namespace Common.Modules.User;

public interface IUserRepository : IGenericRepository<AppUser>
{
    Task<PaginatedList<UserListDto>> GetUserLists(UserListFilterDto filter, CancellationToken ct = default);
}