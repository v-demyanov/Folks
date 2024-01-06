using AutoMapper;

using MongoDB.Bson;

using MediatR;

using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Extensions;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetChannelQuery;

public class GetChannelQueryHandler : IRequestHandler<GetChannelQuery, ChannelDto>
{
    private readonly ChannelsServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetChannelQueryHandler(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<ChannelDto> Handle(GetChannelQuery request, CancellationToken cancellationToken)
    {
        var channelDto = request.Type switch
        {
            ChannelType.Chat => HandleChat(request),
            ChannelType.Group => HandleGroup(request),
            _ => throw new ArgumentOutOfRangeException(nameof(request.Type)),
        };

        return Task.FromResult(channelDto);
    }

    private ChannelDto HandleChat(GetChannelQuery request)
    {
        var chat = _dbContext.Chats.GetById(ObjectId.Parse(request.Id));
        chat.Users = _dbContext.Users.GetByChatId(chat.Id).ToList();

        return _mapper.Map<ChannelDto>(chat);
    }

    private ChannelDto HandleGroup(GetChannelQuery request)
    {
        var group = _dbContext.Groups.GetById(ObjectId.Parse(request.Id));
        return _mapper.Map<ChannelDto>(group);
    }
}
