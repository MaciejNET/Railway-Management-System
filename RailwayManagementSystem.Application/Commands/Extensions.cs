using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands;

public static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        var applicationAssembly = Assembly.GetCallingAssembly();

        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.Scan(s => s.FromAssemblies()
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}