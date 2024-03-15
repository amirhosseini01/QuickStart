using System.Security.Claims;
using Common.Modules.User;

namespace Api;

// A scoped service that exposes the current user information
public class CurrentUser
{
    public AppUser? User { get; set; }
    public ClaimsPrincipal Principal { get; set; } = default!;

    public string Id => this.Principal.FindFirstValue(ClaimTypes.NameIdentifier)!;
    public bool IsAdmin => this.Principal.IsInRole("admin");
}