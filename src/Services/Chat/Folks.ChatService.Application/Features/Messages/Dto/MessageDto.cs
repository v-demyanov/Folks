using Folks.ChatService.Application.Features.Users.Dto;

namespace Folks.ChatService.Application.Features.Messages.Dto;

public record class MessageDto
{
    public required string Id { get; init; }

    public required string Content { get; init; }

    public DateTimeOffset SentAt { get; init; }

    public required string? ChannelId { get; init; }

    public required UserDto Owner { get; init; }
}
