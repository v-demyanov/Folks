using Folks.ChannelsService.Application.Common.Abstractions;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelRemovedNotification;

public class ChannelRemovedNotification : Notification
{
    public required string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }

    public required string ChannelTitle { get; init; }
}
