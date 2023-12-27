using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;
using Folks.ChannelsService.Application.Features.Users.Common.Dto;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class MessageOwnerIdValueResolver : IValueResolver<Message, MessageDto, UserDto>
{
    private readonly ChannelsServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public MessageOwnerIdValueResolver(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public UserDto Resolve(Message source, MessageDto destination, UserDto destMember, ResolutionContext context)
    {
        var owner = _dbContext.Users.GetById(source.OwnerId);
        return _mapper.Map<UserDto>(owner);
    }
}
