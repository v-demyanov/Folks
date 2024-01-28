using AutoMapper;

using Microsoft.AspNetCore.Http;

using System.Security.Claims;

using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class ChatTitleValueResolver : IValueResolver<Chat, ChannelDto, string>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChatTitleValueResolver(IHttpContextAccessor httpContextAccessor)
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
