using Common.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Common.Modules.User;

[ApiController]
[Route("[controller]")]
[ValidateModel]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    public UsersController(UserService userService) =>
        _userService = userService;

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<UserListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get([FromQuery] UserListFilterDto filter, CancellationToken ct)
    {
        var Users = await _userService.GetAdminList(filter: filter, ct: ct);
        return TypedResults.Ok(Users);
    }
}