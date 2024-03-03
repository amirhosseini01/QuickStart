using Api.Common;
using Api.Migrations.User;

namespace Api.Modules.Users;

public class UserRepository : GenericRepository<AppUser>, IUserRepository
{
    public UserRepository(ApiDbContext context) : base(context)
    {
    }
}