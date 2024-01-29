// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Api.Common.Models;

public record class SendMessageRequest
{
    required public string OwnerId { get; init; }

    required public string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }

    required public string Content { get; init; }

    required public DateTimeOffset SentAt { get; init; }
}
