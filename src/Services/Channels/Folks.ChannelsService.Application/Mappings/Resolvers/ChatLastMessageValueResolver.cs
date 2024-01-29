// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class ChatLastMessageValueResolver : IValueResolver<Chat, ChannelDto, MessageDto?>
{
    private readonly IMapper mapper;
    private readonly ChannelsServiceDbContext dbContext;

    public ChatLastMessageValueResolver(IMapper mapper, ChannelsServiceDbContext dbContext)
    {
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public MessageDto? Resolve(Chat source, ChannelDto destination, MessageDto? destMember, ResolutionContext context)
    {
        var lastMessage = this.dbContext.Messages
            .GetByChatId(source.Id)
            .AsEnumerable()
            .LastOrDefault();

        if (lastMessage is not null)
        {
            return this.mapper.Map<MessageDto>(lastMessage);
        }

        return null;
    }
}
