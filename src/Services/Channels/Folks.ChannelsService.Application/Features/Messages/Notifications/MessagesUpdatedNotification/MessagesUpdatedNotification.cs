// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Common.Contracts;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Messages.Notifications.MessagesUpdatedNotification;

public class MessagesUpdatedNotification : Notification
{
    required public IEnumerable<MessageDto> Messages { get; init; }
}
