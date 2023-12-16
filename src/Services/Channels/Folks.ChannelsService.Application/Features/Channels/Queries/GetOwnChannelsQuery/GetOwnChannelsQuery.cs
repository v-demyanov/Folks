using MediatR;

using Folks.ChannelsService.Application.Features.Channels.Dto;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

public class GetOwnChannelsQuery : IRequest<IEnumerable<ChannelDto>>
{
    public required string OwnerId { get; set; }
}
