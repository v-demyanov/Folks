using MediatR;

using Microsoft.AspNetCore.SignalR;

using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelRemovedNotification;
using Folks.ChannelsService.Api.Common.Enums;

namespace Folks.ChannelsService.Api.Hubs;

public class ChannelRemovedNotificationHubHandler : INotificationHandler<ChannelRemovedNotification>
{
    private readonly IHubContext<ChannelsHub> _channelsHubContext;

    public ChannelRemovedNotificationHubHandler(IHubContext<ChannelsHub> channelsHubContext)
    {
        _channelsHubContext = channelsHubContext ?? throw new ArgumentNullException(nameof(channelsHubContext));
    }

    public Task Handle(ChannelRemovedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var userId in notification.Recipients)
        {
            _ = _channelsHubContext.Clients
                .Group(userId)
                .SendAsync(nameof(ChannelsHubEvent.ChannelRemoved), notification);
        }

        return Task.CompletedTask;
    }
}
