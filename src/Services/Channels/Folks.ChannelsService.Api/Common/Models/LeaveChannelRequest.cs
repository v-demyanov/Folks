// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Api.Common.Models;

public record class LeaveChannelRequest
{
    required public string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }
}
