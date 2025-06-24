using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Unistream.Domain.Exceptions.Base;

namespace Unistream.Api.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private const string MostImportantDocumentationEver = "https://youtu.be/dQw4w9WgXcQ?si=-dksc7LLunhtBpXE";
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = exception switch
        {
            BadRequestException or ValidationException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var unistreamException = exception as UnistreamBaseException;

        var response = new
        {
            type = MostImportantDocumentationEver,
            title = exception.Message,
            status = httpContext.Response.StatusCode,
            detail = unistreamException?.Detail,
            instance = $"{httpContext.Request.Path}{httpContext.Request.QueryString}"
        };

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
