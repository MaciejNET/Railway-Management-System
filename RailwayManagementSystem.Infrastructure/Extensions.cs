using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Infrastructure.DAL;
using RailwayManagementSystem.Infrastructure.Exceptions;

namespace RailwayManagementSystem.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ExceptionMiddleware>();
        
        services.AddPostgres(configuration);

        var infrastructureAssembly = typeof(RailwayManagementSystemDbContext).Assembly;

        services.Scan(s => s.FromAssemblies(infrastructureAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}