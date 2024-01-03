using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Application.Features.Channels.Common.Dto;

public record class ChannelDto
{
    public required string Id { get; init; }

    public required string Title { get; init; }

    public ChannelType Type { get; init; }

    public DateTimeOffset CreatedAt { get; init; }
}
