using AutoMapper;

using MediatR;

using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

public class GetOwnChannelsQueryHandler : IRequestHandler<GetOwnChannelsQuery, IEnumerable<ChannelDto>>
{
    private readonly ChannelsServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOwnChannelsQueryHandler(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<IEnumerable<ChannelDto>> Handle(GetOwnChannelsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _dbContext.Users.GetBySourceId(request.OwnerId);

        var chats = _dbContext.Chats.GetByIds(currentUser.ChatIds);
        var groups = _dbContext.Groups.GetByIds(currentUser.GroupIds);

        foreach (var chat in chats)
        {
            chat.Users = _dbContext.Users.GetByChatId(chat.Id).ToList();
        }

        var mappedChats = _mapper.Map<List<ChannelDto>>(chats);
        var mappedGroups = _mapper.Map<List<ChannelDto>>(groups);
        var channels = mappedChats.Union(mappedGroups);

        return Task.FromResult(channels);
    }
}
