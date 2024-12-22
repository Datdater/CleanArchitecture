using Infrastructures.RedisCache.Implementation;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using UseCases;

namespace Infrastructures.RedisCache;

public static class DependencyInjection
{
    public static IServiceCollection AddRedisCacheService(this IServiceCollection services, string redisConnection)
    {
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}