// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Channels.Common.Dto;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;

public record class CreateGroupCommand : IRequest<ChannelDto>
{
    required public string Title { get; init; }

    required public string OwnerId { get; init; }

    required public IEnumerable<string> UserIds { get; init; }
}
