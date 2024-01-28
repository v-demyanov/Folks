using AutoMapper;

using Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;
using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Common.Contracts;

namespace Folks.ChannelsService.Api.Mappings.Resolvers;

public class LeaveChannelCommandUserIdValueResolver : IValueResolver<LeaveChannelRequest, LeaveChannelCommand, string>
{
    private readonly ICurrentUserService _currentUserService;

    public LeaveChannelCommandUserIdValueResolver(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public string Resolve(LeaveChannelRequest source, LeaveChannelCommand destination, string destMember, ResolutionContext context) =>
        _currentUserService.GetUserId() ?? string.Empty;
}
