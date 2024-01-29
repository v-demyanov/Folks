// Copyright (c) v-demyanov. All rights reserved.

namespace Folks.EventBus.Messages.IdentityService;

public record UserRegisteredEvent
{
    required public string UserId { get; init; }

    required public string UserName { get; init; }

    required public string Email { get; init; }
}
