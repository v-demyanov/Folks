// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;

public record class GetMessagesByChannelQuery : IRequest<IEnumerable<MessageDto>>
{
    required public string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }
}
