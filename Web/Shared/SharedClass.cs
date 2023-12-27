/* Shared classes can be referenced by both the Client and Server */
using System.ComponentModel.DataAnnotations;

public class UserInfo
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}

public class ExternalUserInfo
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string ProviderKey { get; set; } = default!;
}

public record AuthToken(string Token);