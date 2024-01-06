using MediatR;

using Microsoft.AspNetCore.SignalR;

using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelUpdatedNotitfication;
using Folks.ChannelsService.Api.Common.Enums;

namespace Folks.ChannelsService.Api.Hubs;

public class ChannelUpdatedNotificationHubHandler : INotificationHandler<ChannelUpdatedNotification>
{
    private readonly IHubContext<ChannelsHub> _channelsHubContext;

    public ChannelUpdatedNotificationHubHandler(IHubContext<ChannelsHub> channelsHubContext)
    {
        _channelsHubContext = channelsHubContext ?? throw new ArgumentNullException(nameof(channelsHubContext));
    }

    public Task Handle(ChannelUpdatedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var userId in notification.Recipients)
        {
            _ = _channelsHubContext.Clients
                .Group(userId)
                .SendAsync(nameof(ChannelsHubEvent.ChannelUpdated), notification.ChannelDto);
        }

        return Task.CompletedTask;
    }
}
