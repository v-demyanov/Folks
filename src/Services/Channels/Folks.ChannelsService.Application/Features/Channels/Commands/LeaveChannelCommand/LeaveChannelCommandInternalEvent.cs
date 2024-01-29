// Copyright (c) v-demyanov. All rights reserved.

namespace Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;

public enum LeaveChannelCommandInternalEvent
{
    ChannelRemoved,
    UserLeftChannel,
    NewGroupOwnerSet,
}