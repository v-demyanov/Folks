using MediatR;

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;

public record class GetMessagesByChannelQuery : IRequest<IEnumerable<MessageDto>>
{
    public required string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }
}
