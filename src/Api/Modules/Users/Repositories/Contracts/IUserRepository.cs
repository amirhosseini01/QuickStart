using Api.Common;
using Api.Modules.Users;

namespace Api.Migrations.User;

public interface IUserRepository: IGenericRepository<AppUser>
{
    Task<PaginatedList<UserListDto>> GetUserLists(UserListFilterDto filter, CancellationToken cancellationToken = default);
}