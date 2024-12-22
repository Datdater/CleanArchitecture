using API.Mapper;
using Infrastructures.SQLServer.DataContext;
using UseCase;
using UseCases.Implementation;
using Microsoft.EntityFrameworkCore;
namespace API;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IManageStudent, ManageStudent>();
        services.AddScoped<IUserService, UserService>();
        services.AddAutoMapper(typeof(MapUIModel).Assembly);

    }
}