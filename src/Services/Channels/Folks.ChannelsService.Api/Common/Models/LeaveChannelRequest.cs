using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Api.Common.Models;

public record class LeaveChannelRequest
{
    public required string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }
}
