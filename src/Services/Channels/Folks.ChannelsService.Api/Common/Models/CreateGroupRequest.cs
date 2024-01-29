// Copyright (c) v-demyanov. All rights reserved.

namespace Folks.ChannelsService.Api.Common.Models;

public record class CreateGroupRequest
{
    required public string Title { get; init; }

    required public IEnumerable<string> UserIds { get; init; }
}
