// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Infrastructure.Persistence;

using MediatR;
using MongoDB.Bson;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetChannelQuery;

public class GetChannelQueryHandler : IRequestHandler<GetChannelQuery, ChannelDto>
{
    private readonly ChannelsServiceDbContext dbContext;
    private readonly IMapper mapper;

    public GetChannelQueryHandler(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<ChannelDto> Handle(GetChannelQuery request, CancellationToken cancellationToken)
    {
        var channelDto = request.Type switch
        {
            ChannelType.Chat => this.HandleChat(request),
            ChannelType.Group => this.HandleGroup(request),
            _ => throw new ArgumentOutOfRangeException(nameof(request.Type)),
        };

        return Task.FromResult(channelDto);
    }

    private ChannelDto HandleChat(GetChannelQuery request)
    {
        var chat = this.dbContext.Chats.GetById(ObjectId.Parse(request.Id));
        chat.Users = this.dbContext.Users.GetByChatId(chat.Id).ToList();

        return this.mapper.Map<ChannelDto>(chat);
    }

    private ChannelDto HandleGroup(GetChannelQuery request)
    {
        var group = this.dbContext.Groups.GetById(ObjectId.Parse(request.Id));
        return this.mapper.Map<ChannelDto>(group);
    }
}
