using Api.Common;
using Api.Migrations.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Users;

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
    public async Task<IResult> Get([FromQuery] UserListFilterDto filter, CancellationToken cancellationToken)
    {
        var Users = await _userRepository.GetUserLists(filter: filter, cancellationToken: cancellationToken);
        return TypedResults.Ok(Users);
    }
}