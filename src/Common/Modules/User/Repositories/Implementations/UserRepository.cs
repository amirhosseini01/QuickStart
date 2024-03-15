using Common.Commons;
using Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.User;

public class UserRepository : GenericRepository<AppUser>, IUserRepository
{
    private readonly DbSet<AppUser> _entities;
    public UserRepository(ApiDbContext context) : base(context) =>
        _entities = context.Users;

    public async Task<PaginatedList<UserListDto>> GetUserLists(UserListFilterDto filter, CancellationToken cancellationToken = default)
    {
        var query = _entities.AsNoTracking();
        return await PaginatedList<UserListDto>.CreateAsync(source: query.MapUserToListDto(), filter: filter, cancellationToken: cancellationToken);
    }
}