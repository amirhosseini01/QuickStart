using System.ComponentModel.DataAnnotations;

namespace Api.Modules.Users;

// This is the DTO used to exchange username and password details to 
// the create user and token endpoints
public class UserInfo
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}