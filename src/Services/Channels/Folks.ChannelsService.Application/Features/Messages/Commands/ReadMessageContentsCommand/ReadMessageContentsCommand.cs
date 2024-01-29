// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.ReadMessageContentsCommand;

public class ReadMessageContentsCommand : IRequest<bool>
{
    required public IEnumerable<string> MessageIds { get; init; }

    required public string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }

    required public string UserId { get; init; }
}
