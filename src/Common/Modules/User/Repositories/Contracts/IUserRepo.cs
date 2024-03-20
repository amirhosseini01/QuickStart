using Common.Commons;

namespace Common.Modules.User;

public interface IUserRepo : IGenericRepository<AppUser>
{
    IQueryable<AppUser> FilterQuery(UserListFilterDto? filter = null);
}