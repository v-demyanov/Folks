using Folks.ChannelsService.Application.Common.Abstractions;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Messages.Notifications.MessageCreatedNotification;

public class MessageCreatedNotification : Notification
{
    public required MessageDto MessageDto { get; init; }
}
