using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;

public record class LeaveChannelCommandResult
{
    public required IEnumerable<LeaveChannelCommandInternalEvent> Events { get; init; }

    public required ChannelPreviousState PreviousState { get; init; }

    public record ChannelPreviousState(IEnumerable<string> UserIds, string ChannelId, ChannelType ChannelType, string Title);
}