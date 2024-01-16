using System.Security.Claims;
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
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // TODO: Support remote logout
        })
        .RequireAuthorization();

        // External login
        group.MapGet("login/{provider}", (string provider) =>
        {
            // Trigger the external login flow by issuing a challenge with the provider name.
            // This name maps to the registered authentication scheme names in AuthenticationExtensions.cs
            return Results.Challenge(
                properties: new() { RedirectUri = $"/auth/signin/{provider}" },
                authenticationSchemes: new[] { provider });
        });

        group.MapGet("signin/{provider}", async (string provider, AuthClient client, HttpContext context) =>
        {
            // Grab the login information from the external login dance
            var result = await context.AuthenticateAsync(AuthenticatonSchemes.ExternalScheme);

            if (result.Succeeded)
            {
                var principal = result.Principal;

                var id = principal.FindFirstValue(ClaimTypes.NameIdentifier)!;

                // TODO: We should have the user pick a user name to complete the external login dance
                // for now we'll prefer the email address
                var name = (principal.FindFirstValue(ClaimTypes.Email) ?? principal.Identity?.Name)!;

                var token = await client.GetOrCreateUserAsync(provider, new() { Username = name, ProviderKey = id });

                if (token is not null)
                {
                    // Write the login cookie
                    await SignIn(id, name, token, provider).ExecuteAsync(context);
                }
            }

            // Delete the external cookie
            await context.SignOutAsync(AuthenticatonSchemes.ExternalScheme);

            // TODO: Handle the failure somehow

            return Results.Redirect("/");
        });

        return group;
    }

    private static IResult SignIn(UserInfo userInfo, string token)
    {
        return SignIn(userInfo.Username, userInfo.Username, token, providerName: null);
    }

    private static IResult SignIn(string userId, string userName, string token, string? providerName)
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
