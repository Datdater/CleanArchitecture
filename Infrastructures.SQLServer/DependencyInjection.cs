using Entities;
using Infrastructures.MapperProfile;
using Infrastructures.SQLServer.DataContext;
using Infrastructures.SQLServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UseCase.Repositories;
using UseCases.GenericRepository;
using UseCases.UnitOfWork;

namespace Infrastructures.SQLServer;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGenericRepository<Student>, GenericRepository<Student, StudentEntity>>();
        services.AddScoped<IStudentRepository, StudentRepository>();

        // ATTENTION: if you do migration please check file README.md
        services.AddDbContext<SchoolContext>(option => option.UseSqlServer(databaseConnection));

        // this configuration just use in-memory for fast develop
        //services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase("test"));

        services.AddAutoMapper(typeof(MapperEntitiesToDto).Assembly);

        return services;
    }

}