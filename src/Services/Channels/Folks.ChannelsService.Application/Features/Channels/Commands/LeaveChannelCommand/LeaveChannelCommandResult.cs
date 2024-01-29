// Copyright (c) v-demyanov. All rights reserved.

using System.Text.Json.Serialization;

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;

[JsonDerivedType(typeof(LeaveChannelCommandSuccessResult))]
[JsonDerivedType(typeof(LeaveChannelCommandErrorResult))]
public record class LeaveChannelCommandResult
{
    public ChannelType ChannelType { get; init; }

    required public string ChannelId { get; init; }

    public string? ChannelTitle { get; init; }
}

public record class LeaveChannelCommandSuccessResult : LeaveChannelCommandResult
{
    required public IDictionary<LeaveChannelCommandInternalEvent, HashSet<string>> Events { get; init; }
}

public record class LeaveChannelCommandErrorResult : LeaveChannelCommandResult
{
    required public string Error { get; init; }
}