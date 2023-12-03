namespace Folks.ChatService.Api.Models;

public record class ErrorResponse
{
    public int StatusCode { get; init; }

    public required string Title { get; init; }

    public IDictionary<string, string[]> Errors { get; init; } = new Dictionary<string, string[]>();
}
