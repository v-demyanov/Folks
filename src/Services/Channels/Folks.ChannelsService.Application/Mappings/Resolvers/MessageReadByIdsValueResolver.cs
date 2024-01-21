using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;
using Folks.ChannelsService.Application.Features.Users.Common.Dto;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class MessageReadByIdsValueResolver : IValueResolver<Message, MessageDto, IEnumerable<UserDto>>
{
    private readonly ChannelsServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public MessageReadByIdsValueResolver(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public IEnumerable<UserDto> Resolve(Message source, MessageDto destination, IEnumerable<UserDto> destMember, ResolutionContext context)
    {
        var users = _dbContext.Users.GetByIds(source.ReadByIds);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
