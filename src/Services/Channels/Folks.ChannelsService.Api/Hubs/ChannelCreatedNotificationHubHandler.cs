using MediatR;

using Microsoft.AspNetCore.SignalR;

using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelCreatedNotification;
using Folks.ChannelsService.Api.Common.Enums;

namespace Folks.ChannelsService.Api.Hubs;

public class ChannelCreatedNotificationHubHandler : INotificationHandler<ChannelCreatedNotification>
{
    private readonly IHubContext<ChannelsHub> _channelsHubContext;

    public ChannelCreatedNotificationHubHandler(IHubContext<ChannelsHub> channelsHubContext)
    {
        _channelsHubContext = channelsHubContext ?? throw new ArgumentNullException(nameof(channelsHubContext));
    }

    public Task Handle(ChannelCreatedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var userId in notification.Recipients)
        {
            _ = _channelsHubContext.Clients
                .Group(userId)
                .SendAsync(nameof(ChannelsHubEvent.ChannelCreated), notification.ChannelDto);
        }
        
        return Task.CompletedTask;
    }
}
