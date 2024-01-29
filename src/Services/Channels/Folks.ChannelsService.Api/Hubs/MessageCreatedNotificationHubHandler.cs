// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Api.Common.Enums;
using Folks.ChannelsService.Application.Features.Messages.Notifications.MessageCreatedNotification;

using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Folks.ChannelsService.Api.Hubs;

public class MessageCreatedNotificationHubHandler : INotificationHandler<MessageCreatedNotification>
{
    private readonly IHubContext<ChannelsHub> channelsHubContext;

    public MessageCreatedNotificationHubHandler(IHubContext<ChannelsHub> channelsHubContext)
    {
        this.channelsHubContext = channelsHubContext ?? throw new ArgumentNullException(nameof(channelsHubContext));
    }

    public Task Handle(MessageCreatedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var userId in notification.Recipients)
        {
            _ = this.channelsHubContext.Clients
                .Group(userId)
                .SendAsync(nameof(ChannelsHubEvent.MessageSent), notification.MessageDto);
        }

        return Task.CompletedTask;
    }
}
