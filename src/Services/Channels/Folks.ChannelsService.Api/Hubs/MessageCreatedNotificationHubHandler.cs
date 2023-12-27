using MediatR;

using Microsoft.AspNetCore.SignalR;

using Folks.ChannelsService.Application.Features.Messages.Notifications.MessageCreatedNotification;
using Folks.ChannelsService.Api.Common.Enums;

namespace Folks.ChannelsService.Api.Hubs;

public class MessageCreatedNotificationHubHandler : INotificationHandler<MessageCreatedNotification>
{
    private readonly IHubContext<ChannelsHub> _channelsHubContext;

    public MessageCreatedNotificationHubHandler(IHubContext<ChannelsHub> channelsHubContext)
    {
        _channelsHubContext = channelsHubContext ?? throw new ArgumentNullException(nameof(channelsHubContext));
    }

    public Task Handle(MessageCreatedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var userId in notification.Recipients)
        {
            _ = _channelsHubContext.Clients
                .Group(userId)
                .SendAsync(nameof(ChannelsHubEvent.MessageSent), notification.MessageDto);
        }

        return Task.CompletedTask;
    }
}
