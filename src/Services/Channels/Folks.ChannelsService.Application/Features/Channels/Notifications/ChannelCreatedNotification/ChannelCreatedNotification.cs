// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Common.Contracts;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelCreatedNotification;

public class ChannelCreatedNotification : Notification
{
    required public ChannelDto ChannelDto { get; init; }
}
