namespace Folks.ChatService.Api.Models;

public record class ErrorResponse
{
    public int StatusCode { get; init; }

    public required string Title { get; init; }

    public object? Errors { get; init; }
}
