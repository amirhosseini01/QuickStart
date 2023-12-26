using System.Security.Claims;

namespace Api;

// A scoped service that exposes the current user information
public class CurrentUser
{
    public ApiUser? User { get; set; }
    public ClaimsPrincipal Principal { get; set; } = default!;

    public string Id => Principal.FindFirstValue(ClaimTypes.NameIdentifier)!;
    public bool IsAdmin => Principal.IsInRole("admin");
}