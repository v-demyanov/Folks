// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Channels.Common.Dto;

public record class ChannelDto
{
    required public string Id { get; init; }

    required public string Title { get; init; }

    public ChannelType Type { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public MessageDto? LastMessage { get; init; }

    public int UnreadMessagesCount { get; init; }
}
