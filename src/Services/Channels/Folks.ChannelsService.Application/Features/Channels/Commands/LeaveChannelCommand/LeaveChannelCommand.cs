using MediatR;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;

public class LeaveChannelCommand : IRequest<LeaveChannelCommandResult>
{
    public required string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }

    public required string UserId { get; init; }
}
