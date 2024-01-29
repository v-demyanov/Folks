// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;

public class LeaveChannelCommand : IRequest<LeaveChannelCommandResult>
{
    required public string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }

    required public string UserId { get; init; }
}
