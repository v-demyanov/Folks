using AutoMapper;

using Folks.ChannelsService.Application.Common.Contracts;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class ChatUnreadMessagesCountValueResolver : IValueResolver<Chat, ChannelDto, int>
{
    private readonly ChannelsServiceDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public ChatUnreadMessagesCountValueResolver(ChannelsServiceDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public int Resolve(Chat source, ChannelDto destination, int destMember, ResolutionContext context)
    {
        var currentUserId = _currentUserService.GetUserId() ?? string.Empty;
        var user = _dbContext.Users.GetBySourceId(currentUserId);

        return _dbContext.Messages
            .AsEnumerable()
            .Where(x => x.ChatId == source.Id && !x.ReadByIds.Contains(user.Id))
            .Count();
    }
}
