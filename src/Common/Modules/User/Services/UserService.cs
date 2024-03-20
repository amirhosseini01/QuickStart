using Common.Commons;

namespace Common.Modules.User;

public class UserService
{
    private readonly IUserRepo _UserRepo;
    public UserService(IUserRepo UserRepository)
    {
        _UserRepo = UserRepository;
    }

    public async Task<PaginatedList<UserListDto>> GetAdminList(UserListFilterDto filter, CancellationToken ct = default)
    {
        return await _UserRepo.GetAdminList(filter: filter, ct: ct);
    }
}