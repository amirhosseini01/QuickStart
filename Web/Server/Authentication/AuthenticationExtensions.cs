using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Web.Server;

public static class AuthenticationExtensions
{
    private delegate void ExternalAuthProvider(AuthenticationBuilder authenticationBuilder, Action<object> configure);

    public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
    {
        // Our default scheme is cookies
        var authenticationBuilder = builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

        // Add the default authentication cookie that will be used between the front end and
        // the backend.
        authenticationBuilder.AddCookie();

        return builder;
    }
}
