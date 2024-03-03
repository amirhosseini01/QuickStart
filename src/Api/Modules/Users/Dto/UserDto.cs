using System.ComponentModel.DataAnnotations;
using Api.Common;

namespace Api.Modules.Users;

public class UserDto
{

}

public class UserInfo
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}

public class UserListDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
}

public class UserListFilterDto: PaginatedListFilter
{
    
}