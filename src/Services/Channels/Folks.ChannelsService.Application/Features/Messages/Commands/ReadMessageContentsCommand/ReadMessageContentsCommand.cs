using MediatR;

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.ReadMessageContentsCommand;

public class ReadMessageContentsCommand : IRequest<bool>
{ 
    public required IEnumerable<string> MessageIds { get; init; }

    public required string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }

    public required string UserId { get; init; }
}
