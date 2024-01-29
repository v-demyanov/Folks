// Copyright (c) v-demyanov. All rights reserved.

using System.Security.Claims;

using AutoMapper;

using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Domain.Entities;

using Microsoft.AspNetCore.Http;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class ChatTitleValueResolver : IValueResolver<Chat, ChannelDto, string>
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public ChatTitleValueResolver(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string Resolve(Chat source, ChannelDto destination, string destMember, ResolutionContext context)
    {
        var currentUserId = this.httpContextAccessor.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (source.Users.Count() == 1)
        {
            return source.Users.First().UserName;
        }

        return source.Users.FirstOrDefault(x => x.SourceId != currentUserId)?.UserName ?? string.Empty;
    }
}
