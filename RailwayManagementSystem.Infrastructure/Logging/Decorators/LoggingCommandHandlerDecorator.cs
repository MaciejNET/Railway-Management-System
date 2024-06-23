using Humanizer;
using Microsoft.Extensions.Logging;
using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Logging.Decorators;

internal sealed class LoggingCommandHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> commandHandler,
    ILogger<LoggingCommandHandlerDecorator<TCommand>> logger)
    : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    public async Task HandleAsync(TCommand command)
    {
        var commandName = typeof(TCommand).Name.Underscore();
        logger.LogInformation("Started handling a command: {CommandName}...", commandName);
        await commandHandler.HandleAsync(command);
        logger.LogInformation("Completed handling a command: {CommandName}...", commandName);
    }
}