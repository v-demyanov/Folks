using AutoMapper;

using System.Security.Claims;

using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;

namespace Folks.ChannelsService.Api.Mappings.Resolvers;

public class CreateGroupRequestOwnerIdValueResolver : IValueResolver<CreateGroupRequest, CreateGroupCommand, string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateGroupRequestOwnerIdValueResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string Resolve(CreateGroupRequest source, CreateGroupCommand destination, string destMember, ResolutionContext context)
    {
        var currentUserId = _httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return currentUserId ?? string.Empty;
    }
}
