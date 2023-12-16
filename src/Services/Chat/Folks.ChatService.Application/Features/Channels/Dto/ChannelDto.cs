using Folks.ChatService.Application.Features.Channels.Enums;

namespace Folks.ChatService.Application.Features.Channels.Dto;

public record class ChannelDto
{
    public required string Id { get; init; }

    public required string Title { get; init; }

    public ChannelType Type { get; init; }
}
