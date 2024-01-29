// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Api.Common.Enums;
using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelCreatedNotification;

using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Folks.ChannelsService.Api.Hubs;

public class ChannelCreatedNotificationHubHandler : INotificationHandler<ChannelCreatedNotification>
{
    private readonly IHubContext<ChannelsHub> channelsHubContext;

    public ChannelCreatedNotificationHubHandler(IHubContext<ChannelsHub> channelsHubContext)
    {
        this.channelsHubContext = channelsHubContext ?? throw new ArgumentNullException(nameof(channelsHubContext));
    }

    public Task Handle(ChannelCreatedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var userId in notification.Recipients)
        {
            _ = this.channelsHubContext.Clients
                .Group(userId)
                .SendAsync(nameof(ChannelsHubEvent.ChannelCreated), notification.ChannelDto);
        }

        return Task.CompletedTask;
    }
}
