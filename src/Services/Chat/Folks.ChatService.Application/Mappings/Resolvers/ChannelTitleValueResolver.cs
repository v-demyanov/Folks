using AutoMapper;

using Microsoft.AspNetCore.Http;

using System.Security.Claims;

using Folks.ChatService.Application.Features.Channels.Dto;
using Folks.ChatService.Domain.Entities;

namespace Folks.ChatService.Application.Mappings.Resolvers;

public class ChannelTitleValueResolver : IValueResolver<Chat, ChannelDto, string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChannelTitleValueResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string Resolve(Chat source, ChannelDto destination, string destMember, ResolutionContext context)
    {
        var currentUserId = _httpContextAccessor.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (source.Users.Count() == 1)
        {
            return source.Users.First().UserName;
        }

        return source.Users.FirstOrDefault(x => x.SourceId != currentUserId)?.UserName ?? string.Empty;
    }
}
