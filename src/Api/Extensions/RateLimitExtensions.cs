﻿using System.Security.Claims;
using System.Threading.RateLimiting;

namespace Api;

public static class RateLimitExtensions
{
    private static readonly string Policy = "PerUserRatelimit";

	public static IServiceCollection AddRateLimiting(this IServiceCollection services) => 
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddPolicy(Policy, context =>
            {
                // We always have a user name
                var username = context.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                return RateLimitPartition.GetTokenBucketLimiter(username, key => new()
				{
					ReplenishmentPeriod = TimeSpan.FromSeconds(10),
					AutoReplenishment = true,
					TokenLimit = 100,
					TokensPerPeriod = 100,
					QueueLimit = 100,
				});
            });
        });

	public static IEndpointConventionBuilder RequirePerUserRateLimit(this IEndpointConventionBuilder builder) => 
        builder.RequireRateLimiting(Policy);
}
