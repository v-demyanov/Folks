// Copyright (c) v-demyanov. All rights reserved.

using System.Security.Claims;

using Folks.ChannelsService.Application.Common.Contracts;

namespace Folks.ChannelsService.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string? GetUserId()
    {
        var currentUserId = this.httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return currentUserId ?? string.Empty;
    }
}
