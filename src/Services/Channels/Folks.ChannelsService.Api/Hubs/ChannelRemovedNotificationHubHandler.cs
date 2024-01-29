// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Api.Common.Enums;
using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelRemovedNotification;

using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Folks.ChannelsService.Api.Hubs;

public class ChannelRemovedNotificationHubHandler : INotificationHandler<ChannelRemovedNotification>
{
    private readonly IHubContext<ChannelsHub> channelsHubContext;

    public ChannelRemovedNotificationHubHandler(IHubContext<ChannelsHub> channelsHubContext)
    {
        this.channelsHubContext = channelsHubContext ?? throw new ArgumentNullException(nameof(channelsHubContext));
    }

    public Task Handle(ChannelRemovedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var userId in notification.Recipients)
        {
            _ = this.channelsHubContext.Clients
                .Group(userId)
                .SendAsync(nameof(ChannelsHubEvent.ChannelRemoved), notification.ChannelDto);
        }

        return Task.CompletedTask;
    }
}
