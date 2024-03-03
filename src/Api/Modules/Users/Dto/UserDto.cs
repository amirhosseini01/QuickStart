using System.ComponentModel.DataAnnotations;

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
    public string UserId { get; set; }
}