﻿using AutoMapper;

using MediatR;

using MongoDB.Bson;

using Folks.ChannelsService.Application.Features.Messages.Dto;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Features.Channels.Enums;
using Folks.ChannelsService.Application.Extensions;

namespace Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;

public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IEnumerable<MessageDto>>
{
    private readonly ChatServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMessagesQueryHandler(ChatServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<IEnumerable<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = request.ChannelType switch
        {
            ChannelType.Group =>
                 _dbContext.Messages
                    .GetByGroupId(ObjectId.Parse(request.ChannelId))
                    .AsEnumerable(),
            ChannelType.Chat =>
                _dbContext.Messages
                    .GetByChatId(ObjectId.Parse(request.ChannelId))
                    .AsEnumerable(),
            _ => throw new ArgumentOutOfRangeException(nameof(request.ChannelType))
        };

        var messagesDto = _mapper.Map<IEnumerable<MessageDto>>(messages);
        return Task.FromResult(messagesDto);
    }
}