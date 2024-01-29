// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;
using Folks.ChannelsService.Domain.Common.Enums;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;

public record class CreateMessageCommand : IRequest<MessageDto>
{
    required public string OwnerId { get; init; }

    required public string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }

    public string? Content { get; init; }

    required public DateTimeOffset SentAt { get; init; }

    public MessageType Type { get; init; } = MessageType.Text;
}