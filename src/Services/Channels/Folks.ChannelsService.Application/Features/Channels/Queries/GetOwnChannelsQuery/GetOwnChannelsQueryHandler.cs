using MediatR;

using AutoMapper;

using MongoDB.Bson;

using Folks.ChannelsService.Application.Features.Channels.Dto;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Domain.Entities;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

public class GetOwnChannelsQueryHandler : IRequestHandler<GetOwnChannelsQuery, IEnumerable<ChannelDto>>
{
    private readonly ChatServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOwnChannelsQueryHandler(ChatServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<IEnumerable<ChannelDto>> Handle(GetOwnChannelsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _dbContext.Users.First(user => user.SourceId == request.OwnerId);

        var chats = GetChatsByIds(currentUser.ChatIds);
        var groups = GetGroupsByIds(currentUser.GroupIds);

        foreach (var chat in chats)
        {
            chat.Users = GetUsersByChatId(chat.Id).ToList();
        }

        var mappedChats = _mapper.Map<List<ChannelDto>>(chats);
        var mappedGroups = _mapper.Map<List<ChannelDto>>(groups);
        var channels = mappedChats.Union(mappedGroups);

        return Task.FromResult(channels);
    }

    // TODO: Move it in IQueryable extensions after mongo-efcore-provider will be updated
    private IEnumerable<Chat> GetChatsByIds(IEnumerable<ObjectId> ids) =>
        _dbContext.Chats
            .AsEnumerable()
            .Where(chat => ids.Any(chatId => chatId == chat.Id));

    // TODO: Move it in IQueryable extensions after mongo-efcore-provider will be updated
    private IEnumerable<Group> GetGroupsByIds(IEnumerable<ObjectId> ids) =>
        _dbContext.Groups
            .AsEnumerable()
            .Where(group => ids.Any(groupId => groupId == group.Id));

    // TODO: Move it in IQueryable extensions after mongo-efcore-provider will be updated
    private IEnumerable<User> GetUsersByChatId(ObjectId chatId) =>
        _dbContext.Users
            .AsEnumerable()
            .Where(user => user.ChatIds.Any(id => id == chatId));
}
