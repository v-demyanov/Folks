// Copyright (c) v-demyanov. All rights reserved.

namespace Folks.ChannelsService.Application.Features.Users.Common.Dto;

public record class UserDto
{
    required public string Id { get; init; }

    required public string UserName { get; init; }

    required public string Email { get; init; }
}
