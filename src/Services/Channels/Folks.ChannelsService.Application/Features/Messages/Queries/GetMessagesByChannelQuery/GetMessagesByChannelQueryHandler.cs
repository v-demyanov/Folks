// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

using MediatR;
using MongoDB.Bson;

namespace Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;

public class GetMessagesByChannelQueryHandler : IRequestHandler<GetMessagesByChannelQuery, IEnumerable<MessageDto>>
{
    private readonly ChannelsServiceDbContext dbContext;
    private readonly IMapper mapper;

    public GetMessagesByChannelQueryHandler(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<IEnumerable<MessageDto>> Handle(GetMessagesByChannelQuery request, CancellationToken cancellationToken)
    {
        var messages = request.ChannelType switch
        {
            ChannelType.Group =>
                this.dbContext.Messages
                    .GetByGroupId(ObjectId.Parse(request.ChannelId))
                    .AsEnumerable(),
            ChannelType.Chat =>
                this.dbContext.Messages
                    .GetByChatId(ObjectId.Parse(request.ChannelId))
                    .AsEnumerable(),
            _ => new List<Message>(),
        };

        var messagesDto = this.mapper.Map<IEnumerable<MessageDto>>(messages);
        return Task.FromResult(messagesDto);
    }
}
