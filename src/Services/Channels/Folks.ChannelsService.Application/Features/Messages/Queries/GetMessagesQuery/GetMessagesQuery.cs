using MediatR;

using Folks.ChannelsService.Application.Features.Messages.Dto;
using Folks.ChannelsService.Application.Features.Channels.Enums;

namespace Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;

public record class GetMessagesQuery : IRequest<IEnumerable<MessageDto>>
{
    public required string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }
}
