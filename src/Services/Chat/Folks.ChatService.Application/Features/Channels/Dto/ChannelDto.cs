namespace Folks.ChatService.Application.Features.Channels.Dto;

public record class ChannelDto
{
    public required string Id { get; init; }

    public string? Title { get; init; }

    public ChannelType Type { get; init; }
}
