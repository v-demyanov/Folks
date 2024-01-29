// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Common.Contracts;
using Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;

namespace Folks.ChannelsService.Api.Mappings.Resolvers;

public class LeaveChannelCommandUserIdValueResolver : IValueResolver<LeaveChannelRequest, LeaveChannelCommand, string>
{
    private readonly ICurrentUserService currentUserService;

    public LeaveChannelCommandUserIdValueResolver(ICurrentUserService currentUserService)
    {
        this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public string Resolve(LeaveChannelRequest source, LeaveChannelCommand destination, string destMember, ResolutionContext context) =>
        this.currentUserService.GetUserId() ?? string.Empty;
}
