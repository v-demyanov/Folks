using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class ChannelGroupLastMessageValueResolver : IValueResolver<Group, ChannelDto, MessageDto?>
{
    private readonly IMapper _mapper;
    private readonly ChannelsServiceDbContext _dbContext;

    public ChannelGroupLastMessageValueResolver(IMapper mapper, ChannelsServiceDbContext dbContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public MessageDto? Resolve(Group source, ChannelDto destination, MessageDto? destMember, ResolutionContext context)
    {
        var lastMessage = _dbContext.Messages
            .GetByGroupId(source.Id)
            .AsEnumerable()
            .LastOrDefault();

        if (lastMessage is not null)
        {
            return _mapper.Map<MessageDto>(lastMessage);
        }

        return null;
    }
}
