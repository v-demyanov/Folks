// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Common.Contracts;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class ChatUnreadMessagesCountValueResolver : IValueResolver<Chat, ChannelDto, int>
{
    private readonly ChannelsServiceDbContext dbContext;
    private readonly ICurrentUserService currentUserService;

    public ChatUnreadMessagesCountValueResolver(ChannelsServiceDbContext dbContext, ICurrentUserService currentUserService)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public int Resolve(Chat source, ChannelDto destination, int destMember, ResolutionContext context)
    {
        var currentUserId = this.currentUserService.GetUserId() ?? string.Empty;
        var user = this.dbContext.Users.GetBySourceId(currentUserId);

        return this.dbContext.Messages
            .AsEnumerable()
            .Where(x => x.ChatId == source.Id && !x.ReadByIds.Contains(user.Id))
            .Count();
    }
}
