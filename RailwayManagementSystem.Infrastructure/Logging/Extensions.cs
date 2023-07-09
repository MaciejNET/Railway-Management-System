using Microsoft.Extensions.DependencyInjection;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Infrastructure.Logging.Decorators;

namespace RailwayManagementSystem.Infrastructure.Logging;

internal static class Extensions
{
    public static IServiceCollection AddCustomLogging(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

        return services;
    }
}