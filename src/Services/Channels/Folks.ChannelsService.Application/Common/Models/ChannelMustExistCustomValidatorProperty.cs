// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Application.Common.Models;

public record class ChannelMustExistCustomValidatorProperty
{
    required public string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }
}
