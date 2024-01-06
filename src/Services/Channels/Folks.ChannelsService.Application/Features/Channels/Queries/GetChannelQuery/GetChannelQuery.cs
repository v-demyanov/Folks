using MediatR;

using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetChannelQuery;

public class GetChannelQuery : IRequest<ChannelDto>
{
    public required string Id { get; init; }

    public ChannelType Type { get; init; }
}
