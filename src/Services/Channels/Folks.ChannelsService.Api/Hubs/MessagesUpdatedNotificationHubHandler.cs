using MediatR;

using Microsoft.AspNetCore.SignalR;

using Folks.ChannelsService.Application.Features.Messages.Notifications.MessagesUpdatedNotification;
using Folks.ChannelsService.Api.Common.Enums;

namespace Folks.ChannelsService.Api.Hubs;

public class MessagesUpdatedNotificationHubHandler : INotificationHandler<MessagesUpdatedNotification>
{
    private readonly IHubContext<ChannelsHub> _channelsHubContext;

    public MessagesUpdatedNotificationHubHandler(IHubContext<ChannelsHub> channelsHubContext)
    {
        _channelsHubContext = channelsHubContext ?? throw new ArgumentNullException(nameof(channelsHubContext));
    }

    public Task Handle(MessagesUpdatedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var userId in notification.Recipients)
        {
            _ = _channelsHubContext.Clients
                .Group(userId)
                .SendAsync(nameof(ChannelsHubEvent.MessagesUpdated), notification.Messages);
        }

        return Task.CompletedTask;
    }
}
