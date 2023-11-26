using MediatR;

using Folks.ChatService.Application.Features.Channels.Dto;

namespace Folks.ChatService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

public class GetOwnChannelsQuery : IRequest<IEnumerable<ChannelDto>>
{
    public required string OwnerId { get; set; }
}
