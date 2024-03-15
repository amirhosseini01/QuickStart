using System.Security.Claims;
using Common.Modules.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Web.Server;

public static class AuthApi
{
    public static RouteGroupBuilder MapAuth(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/auth");

        group.MapPost("register", async (UserInfo userInfo, AuthClient client) =>
        {
            // Retrieve the access token given the user info
            var token = await client.CreateUserAsync(userInfo);

            if (token is null)
            {
                return Results.Unauthorized();
            }

            return SignIn(userInfo, token);
        });

        group.MapPost("login", async (UserInfo userInfo, AuthClient client) =>
        {
            // Retrieve the access token give the user info
            var token = await client.GetTokenAsync(userInfo);

            if (token is null)
            {
                return Results.Unauthorized();
            }

            return SignIn(userInfo, token);
        });

        group.MapPost("logout", async (HttpContext context) =>
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme))
                .RequireAuthorization();

        return group;
    }

    private static IResult SignIn(UserInfo userInfo, string token) => SignIn(userInfo.Username, userInfo.Username, token);

    private static IResult SignIn(string userId, string userName, string token)
    {
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
        identity.AddClaim(new Claim(ClaimTypes.Name, userName));

        var properties = new AuthenticationProperties();

        var tokens = new[]
        {
            new AuthenticationToken { Name = TokenNames.AccessToken, Value = token }
        };

        properties.StoreTokens(tokens);


        return Results.SignIn(new ClaimsPrincipal(identity),
            properties: properties,
            authenticationScheme: CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
