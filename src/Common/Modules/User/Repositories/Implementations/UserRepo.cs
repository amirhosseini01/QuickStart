using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.User;

public class UserRepo : GenericRepository<AppUser>, IUserRepo
{
    private readonly DbSet<AppUser> _entities;
    public UserRepo(ApiDbContext context) : base(context) =>
        _entities = context.Users;

    public IQueryable<AppUser> FilterQuery(UserListFilterDto? filter = null)
    {
        var query = _entities.AsNoTracking();
        return query;
    }
}