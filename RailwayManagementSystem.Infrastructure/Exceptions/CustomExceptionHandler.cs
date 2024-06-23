using Humanizer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RailwayManagementSystem.Core.Exceptions;
using Serilog;

namespace RailwayManagementSystem.Infrastructure.Exceptions;

internal sealed class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not CustomException customException) return false;
 
        logger.LogError(exception, exception.Message);
        httpContext.Response.StatusCode = customException.HttpStatusCode;
        httpContext.Response.ContentType = "application/json";

        var error = new Error
        (
            Code: customException.GetType().Name.Underscore().Replace("_exception", string.Empty),
            Reason: customException.Message
        );

        await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);
        
        return true;
    }
}