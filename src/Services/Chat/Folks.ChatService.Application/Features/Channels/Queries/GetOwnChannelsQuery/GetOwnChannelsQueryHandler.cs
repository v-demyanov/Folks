using MediatR;

using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<ChannelDto>> Handle(GetOwnChannelsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.SourceId == request.OwnerId);
        if (currentUser is null)
        {
            throw new EntityNotFoundException(nameof(User), request.OwnerId);
        }

        var chats = await _dbContext.Chats
            .Where(chat => chat.UserIds.Any(userId => userId == currentUser.Id))
            .ToListAsync();

        var groups = await _dbContext.Groups
            .Where(group => group.UserIds.Any(userId => userId == currentUser.Id))
            .ToListAsync();

        foreach (var chat in chats)
        {
            var users = await _dbContext.Users
                .Where(user => user.ChatIds.Any(chatId => chatId == chat.Id))
                .ToListAsync();

            chat.Users = users;
        }

        var mappedChats = _mapper.Map<List<ChannelDto>>(chats);
        var mappedGroups = _mapper.Map<List<ChannelDto>>(groups);
        var channels = mappedChats.Union(mappedGroups);

        return channels;
    }
}
