using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Infrastructure.Auth;
using RailwayManagementSystem.Infrastructure.DAL;
using RailwayManagementSystem.Infrastructure.DAL.Queries;
using RailwayManagementSystem.Infrastructure.Exceptions;
using RailwayManagementSystem.Infrastructure.Security;

namespace RailwayManagementSystem.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ExceptionMiddleware>();
        services.AddHttpContextAccessor();
        
        services.AddPostgres(configuration);

        services.AddSecurity();

        services.AddQueries();

        services.AddAuth(configuration);
        
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }
}