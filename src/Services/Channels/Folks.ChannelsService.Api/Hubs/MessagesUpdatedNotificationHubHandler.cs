// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Api.Common.Enums;
using Folks.ChannelsService.Application.Features.Messages.Notifications.MessagesUpdatedNotification;

using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Folks.ChannelsService.Api.Hubs;

public class MessagesUpdatedNotificationHubHandler : INotificationHandler<MessagesUpdatedNotification>
{
    private readonly IHubContext<ChannelsHub> channelsHubContext;

    public MessagesUpdatedNotificationHubHandler(IHubContext<ChannelsHub> channelsHubContext)
    {
        this.channelsHubContext = channelsHubContext ?? throw new ArgumentNullException(nameof(channelsHubContext));
    }

    public Task Handle(MessagesUpdatedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var userId in notification.Recipients)
        {
            _ = this.channelsHubContext.Clients
                .Group(userId)
                .SendAsync(nameof(ChannelsHubEvent.MessagesUpdated), notification.Messages);
        }

        return Task.CompletedTask;
    }
}
