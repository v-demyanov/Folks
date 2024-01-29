// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetChannelQuery;

public class GetChannelQuery : IRequest<ChannelDto>
{
    required public string Id { get; init; }

    public ChannelType Type { get; init; }
}
