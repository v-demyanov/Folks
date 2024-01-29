// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Exceptions;

namespace Folks.ChannelsService.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        this.next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = exception switch
            {
                EntityNotFoundException => StatusCodes.Status404NotFound,
                AuthenticationFailedException => StatusCodes.Status401Unauthorized,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError,
            };

            switch (exception)
            {
                case ValidationException validationException:
                    await this.HandleValidationExceptionAsync(context, validationException);
                    break;
                default:
                    await this.HandleByDefaultAsync(context, exception);
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
