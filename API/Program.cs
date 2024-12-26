using System.Reflection;
using System.Text;
using API;
using API.Middleware;
using AutoMapper;
using Infrastructures.MapperProfile;
using Infrastructures.RedisCache;
using Infrastructures.SQLServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UseCase.Commons;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<AppConfiguration>();


builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddInfrastructuresService(configuration.DatabaseConnection);
builder.Services.AddRedisCacheService(configuration.RedisConnection);
builder.Services.AddSingleton(configuration);
var secret = configuration.JWTSecretKey;
var key = Encoding.ASCII.GetBytes(secret);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

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

// Apply routing and controller mapping
app.MapControllers();

// Enable CORS
app.UseCors("AllowAll");

// Enable Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Apply the custom middleware for refresh token validation
app.UseMiddleware<RefreshTokenValidationMiddleware>();
//
app.UseMiddleware<ExceptionMiddleware>();
// Start the application
app.Run();