using Folks.ChatService.Api.Models;
using Folks.ChatService.Application.Exceptions;

namespace Folks.ChatService.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = exception switch
            {
                EntityNotFoundException => StatusCodes.Status404NotFound,
                AuthenticationFailedException => StatusCodes.Status401Unauthorized,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            switch (exception)
            {
                case ValidationException:
                    await HandleValidationExceptionAsync(context, exception as ValidationException ?? new ValidationException());
                    break;
                default:
                    await HandleByDefaultAsync(context, exception);
                    break;
            }
        }
    }

    private async Task HandleByDefaultAsync(HttpContext context, Exception exception)
    {
        await context.Response.WriteAsJsonAsync(new ErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            Title = exception.Message,
        });
    }

    private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        await context.Response.WriteAsJsonAsync(new ErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            Title = exception.Message,
            Errors = exception.Errors,
        });
    }
}
