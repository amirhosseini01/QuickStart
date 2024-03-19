using Common.Commons;
using Common.Modules.User;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.User;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public UsersController(IUserRepository userRepository) =>
        _userRepository = userRepository;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<UserListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] UserListFilterDto filter, CancellationToken ct)
    {
        var Users = await _userRepository.GetUserLists(filter: filter, ct: ct);
        return TypedResults.Ok(Users);
    }
}