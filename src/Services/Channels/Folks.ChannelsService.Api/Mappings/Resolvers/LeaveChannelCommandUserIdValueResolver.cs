using AutoMapper;

using System.Security.Claims;
using Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;
using Folks.ChannelsService.Api.Common.Models;

namespace Folks.ChannelsService.Api.Mappings.Resolvers;

public class LeaveChannelCommandUserIdValueResolver : IValueResolver<LeaveChannelRequest, LeaveChannelCommand, string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LeaveChannelCommandUserIdValueResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string Resolve(LeaveChannelRequest source, LeaveChannelCommand destination, string destMember, ResolutionContext context)
    {
        var currentUserId = _httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return currentUserId ?? string.Empty;
    }
}
