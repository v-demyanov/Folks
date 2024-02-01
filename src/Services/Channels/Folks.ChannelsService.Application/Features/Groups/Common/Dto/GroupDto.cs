// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Features.Users.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Groups.Common.Dto;

public record class GroupDto
{
    required public string Id { get; init; }

    required public string Title { get; init; }

    required public string OwnerId { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    required public IEnumerable<UserDto> Members { get; init; }
}
