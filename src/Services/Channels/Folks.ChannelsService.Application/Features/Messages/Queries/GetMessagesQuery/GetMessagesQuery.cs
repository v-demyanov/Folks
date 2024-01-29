// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Messages.Common.Dto;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;

public class GetMessagesQuery : IRequest<IEnumerable<MessageDto>>
{
    required public IEnumerable<string> MessageIds { get; init; }
}
