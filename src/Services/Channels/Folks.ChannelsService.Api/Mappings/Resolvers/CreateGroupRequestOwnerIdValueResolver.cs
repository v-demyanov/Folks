// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Common.Contracts;
using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;

namespace Folks.ChannelsService.Api.Mappings.Resolvers;

public class CreateGroupRequestOwnerIdValueResolver : IValueResolver<CreateGroupRequest, CreateGroupCommand, string>
{
    private readonly ICurrentUserService currentUserService;

    public CreateGroupRequestOwnerIdValueResolver(ICurrentUserService currentUserService)
    {
        this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public string Resolve(CreateGroupRequest source, CreateGroupCommand destination, string destMember, ResolutionContext context) =>
        this.currentUserService.GetUserId() ?? string.Empty;
}
