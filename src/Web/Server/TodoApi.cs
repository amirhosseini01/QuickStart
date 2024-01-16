using Microsoft.AspNetCore.Authentication;
using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

namespace Web.Server;

public static class ApiHelper
{
    public static RouteGroupBuilder MapApi(this IEndpointRouteBuilder routes, string url)
    {
        // The  API translates the authentication cookie between the browser the BFF into an 
        // access token that is sent to the  API. We're using YARP to forward the request.

        var group = routes.MapGroup("/api");

        group.RequireAuthorization();

        var transformBuilder = routes.ServiceProvider.GetRequiredService<ITransformBuilder>();
        var transform = transformBuilder.Create(b =>
        {
            b.AddRequestTransform(async c =>
            {
                var accessToken = await c.HttpContext.GetTokenAsync(TokenNames.AccessToken);

                c.ProxyRequest.Headers.Authorization = new("Bearer", accessToken);
            });
        });

        group.MapForwarder("{*path}", url, new ForwarderRequestConfig(), transform);

        return group;
    }
}
