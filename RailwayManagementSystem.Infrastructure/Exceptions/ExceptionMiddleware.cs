using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Infrastructure.Exceptions;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (CustomException e)
        {
            _logger.LogError(e, e.Message);
            var (statusCode, error) = (e.HttpStatusCode, new Error(
                e.GetType().Name.Underscore().Replace("_exception", string.Empty), e.Message));

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(error);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            var (statusCode, error) = (StatusCodes.Status500InternalServerError, new Error("error", "There was an error"));
            
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(error);
        }
    }

    private record Error(string Code, string Reason);
}