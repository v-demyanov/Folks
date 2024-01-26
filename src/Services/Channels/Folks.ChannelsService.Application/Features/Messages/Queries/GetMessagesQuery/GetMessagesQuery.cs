using MediatR;

using Folks.ChannelsService.Application.Features.Messages.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;

public class GetMessagesQuery : IRequest<IEnumerable<MessageDto>>
{
    public required IEnumerable<string> MessageIds { get; init; }
}
