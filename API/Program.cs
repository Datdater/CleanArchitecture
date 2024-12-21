using System.Reflection;
using API;
using AutoMapper;
using Infrastructures.MapperProfile;
using Infrastructures.SQLServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddInfrastructuresService(builder.Configuration.GetConnectionString("DefaultConnection"));
// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Swagger UI at the root path
    });
}
app.MapControllers();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.Run();