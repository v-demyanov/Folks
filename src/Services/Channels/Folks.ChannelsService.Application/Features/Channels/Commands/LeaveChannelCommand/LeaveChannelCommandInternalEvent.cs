﻿namespace Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;

public enum LeaveChannelCommandInternalEvent
{
    ChannelRemoved,
    UserLeft,
    NewGroupOwnerSet,
    Error,
}