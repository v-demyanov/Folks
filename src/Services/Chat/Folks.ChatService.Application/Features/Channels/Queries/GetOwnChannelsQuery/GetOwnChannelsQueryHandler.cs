using MediatR;

using AutoMapper;

using Folks.ChatService.Application.Features.Channels.Dto;
using Folks.ChatService.Infrastructure.Persistence;
using Folks.ChatService.Application.Exceptions;
using Folks.ChatService.Domain.Entities;

namespace Folks.ChatService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

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
        var currentUser = _dbContext.Users.FirstOrDefault(user => user.SourceId == request.OwnerId);
        if (currentUser is null)
        {
            throw new EntityNotFoundException(nameof(User), request.OwnerId);
        }

        var chats = _dbContext.Chats
            .AsEnumerable()
            .Where(chat => currentUser.ChatIds.Any(chatId => chatId == chat.Id));

        var groups = _dbContext.Groups
            .AsEnumerable()
            .Where(group => currentUser.GroupIds.Any(groupId => groupId == group.Id));

        foreach (var chat in chats)
        {
            var users = _dbContext.Users
                .AsEnumerable()
                .Where(user => user.ChatIds.Any(chatId => chatId == chat.Id))
                .ToList();

            chat.Users = users;
        }

        var mappedChats = _mapper.Map<List<ChannelDto>>(chats);
        var mappedGroups = _mapper.Map<List<ChannelDto>>(groups);
        var channels = mappedChats.Union(mappedGroups);

        return Task.FromResult(channels);
    }
}
