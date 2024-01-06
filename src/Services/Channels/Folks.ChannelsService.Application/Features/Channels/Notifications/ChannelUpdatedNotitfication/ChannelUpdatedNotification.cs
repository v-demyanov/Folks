using Folks.ChannelsService.Application.Common.Abstractions;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelUpdatedNotitfication;

public class ChannelUpdatedNotification : Notification
{
    public required ChannelDto ChannelDto { get; init; }
}
