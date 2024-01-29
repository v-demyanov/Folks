// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Api.Common.Enums;
using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelUpdatedNotitfication;

using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Folks.ChannelsService.Api.Hubs;

public class ChannelUpdatedNotificationHubHandler : INotificationHandler<ChannelUpdatedNotification>
{
    private readonly IHubContext<ChannelsHub> channelsHubContext;

    public ChannelUpdatedNotificationHubHandler(IHubContext<ChannelsHub> channelsHubContext)
    {
        this.channelsHubContext = channelsHubContext ?? throw new ArgumentNullException(nameof(channelsHubContext));
    }

    public Task Handle(ChannelUpdatedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var userId in notification.Recipients)
        {
            _ = this.channelsHubContext.Clients
                .Group(userId)
                .SendAsync(nameof(ChannelsHubEvent.ChannelUpdated), notification.ChannelDto);
        }

        return Task.CompletedTask;
    }
}
