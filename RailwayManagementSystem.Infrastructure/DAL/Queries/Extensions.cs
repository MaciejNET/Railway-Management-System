using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        var infrastructureAssembly = Assembly.GetCallingAssembly();

        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.Scan(s => s.FromAssemblies(infrastructureAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}