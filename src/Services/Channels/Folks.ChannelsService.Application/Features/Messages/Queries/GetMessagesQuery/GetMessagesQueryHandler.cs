using AutoMapper;

using MediatR;

using MongoDB.Bson;

using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;

public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IEnumerable<MessageDto>>
{
    private readonly ChannelsServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMessagesQueryHandler(ChannelsServiceDbContext dbContext, IMapper mapper)
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
            _ => new List<Message>(),
        };

        var messagesDto = _mapper.Map<IEnumerable<MessageDto>>(messages);
        return Task.FromResult(messagesDto);
    }
}
