// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Channels.Common.Dto;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

public class GetOwnChannelsQuery : IRequest<IEnumerable<ChannelDto>>
{
    required public string OwnerId { get; set; }
}
