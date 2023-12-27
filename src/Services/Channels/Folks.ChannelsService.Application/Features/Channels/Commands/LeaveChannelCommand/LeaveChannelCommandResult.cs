using System.Text.Json.Serialization;

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;

[JsonDerivedType(typeof(LeaveChannelCommandSuccessResult))]
[JsonDerivedType(typeof(LeaveChannelCommandErrorResult))]
public record class LeaveChannelCommandResult
{
    public ChannelType ChannelType { get; init; }

    public required string ChannelId { get; init; }

    public string? ChannelTitle { get; init; }
}

public record class LeaveChannelCommandSuccessResult : LeaveChannelCommandResult
{
    public required IEnumerable<LeaveChannelCommandInternalEvent> Events { get; init; }

    public required IEnumerable<string> Recipients { get; init; }
}

public record class LeaveChannelCommandErrorResult : LeaveChannelCommandResult
{
    public required string Error { get; init; }
}