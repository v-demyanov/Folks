// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Common.Contracts;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Messages.Notifications.MessageCreatedNotification;

public class MessageCreatedNotification : Notification
{
    required public MessageDto MessageDto { get; init; }
}
