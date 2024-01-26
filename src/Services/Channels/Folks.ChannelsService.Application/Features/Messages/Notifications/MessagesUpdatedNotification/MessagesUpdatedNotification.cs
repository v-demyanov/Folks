﻿using Folks.ChannelsService.Application.Common.Abstractions;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Messages.Notifications.MessagesUpdatedNotification;

public class MessagesUpdatedNotification : Notification
{
    public required IEnumerable<MessageDto> Messages { get; init; }
}
