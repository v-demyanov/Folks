// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Infrastructure.Persistence;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

public class GetOwnChannelsQueryHandler : IRequestHandler<GetOwnChannelsQuery, IEnumerable<ChannelDto>>
{
    private readonly ChannelsServiceDbContext dbContext;
    private readonly IMapper mapper;

    public GetOwnChannelsQueryHandler(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<IEnumerable<ChannelDto>> Handle(GetOwnChannelsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = this.dbContext.Users.GetBySourceId(request.OwnerId);

        var chats = this.dbContext.Chats.GetByIds(currentUser.ChatIds);
        var groups = this.dbContext.Groups.GetByIds(currentUser.GroupIds);

        foreach (var chat in chats)
        {
            chat.Users = this.dbContext.Users.GetByChatId(chat.Id).ToList();
        }

        var mappedChats = this.mapper.Map<List<ChannelDto>>(chats);
        var mappedGroups = this.mapper.Map<List<ChannelDto>>(groups);
        var channels = mappedChats.Union(mappedGroups);

        return Task.FromResult(channels);
    }
}
