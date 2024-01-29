// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Users.Common.Dto;
using Folks.ChannelsService.Domain.Common.Enums;

namespace Folks.ChannelsService.Application.Features.Messages.Common.Dto;

public record class MessageDto
{
    required public string Id { get; init; }

    public string? Content { get; init; }

    public DateTimeOffset SentAt { get; init; }

    required public string ChannelId { get; init; }

    required public UserDto Owner { get; init; }

    public MessageType Type { get; init; }

    required public IEnumerable<UserDto> ReadBy { get; init; }
}
