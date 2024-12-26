using StackExchange.Redis;

namespace API.Middleware;

public class RefreshTokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConnectionMultiplexer _redis;

    public RefreshTokenValidationMiddleware(RequestDelegate next, IConnectionMultiplexer redis)
    {
        _next = next;
        _redis = redis;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip refresh token validation for logout route or any public route
        if (context.Request.Path.StartsWithSegments("/api/user") ||
            context.Request.Path.StartsWithSegments("/api/user") || 
            context.Request.Path.StartsWithSegments("/api/student"))
        {
            await _next(context); // Skip validation for logout endpoint
            return;
        }
        var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(accessToken))
        {
            var db = _redis.GetDatabase();

            // Get associated refresh token
            var associatedRefreshToken = await db.StringGetAsync($"accessToken:{accessToken}");
            if (associatedRefreshToken.IsNullOrEmpty)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid or expired access token.");
                return;
            }

            // Check if the associated refresh token is revoked
            var isRevoked = await db.StringGetAsync(associatedRefreshToken.ToString());
            if (isRevoked.IsNullOrEmpty)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Access token is invalid due to revoked refresh token.");
                return;
            }
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Access token is required.");
            return;
        }

        // Proceed to the next middleware
        await _next(context);
    }
}
