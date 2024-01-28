using AutoMapper;

using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;
using Folks.ChannelsService.Application.Common.Contracts;

namespace Folks.ChannelsService.Api.Mappings.Resolvers;

public class CreateGroupRequestOwnerIdValueResolver : IValueResolver<CreateGroupRequest, CreateGroupCommand, string>
{
    private readonly ICurrentUserService _currentUserService;

    public CreateGroupRequestOwnerIdValueResolver(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public string Resolve(CreateGroupRequest source, CreateGroupCommand destination, string destMember, ResolutionContext context) =>
        _currentUserService.GetUserId() ?? string.Empty;
}
