using AutoMapper;

using Folks.ChatService.Application.Extensions;
using Folks.ChatService.Application.Features.Messages.Dto;
using Folks.ChatService.Application.Features.Users.Dto;
using Folks.ChatService.Domain.Entities;
using Folks.ChatService.Infrastructure.Persistence;

namespace Folks.ChatService.Application.Mappings.Resolvers;

public class MessageOwnerIdValueResolver : IValueResolver<Message, MessageDto, UserDto>
{
    private readonly ChatServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public MessageOwnerIdValueResolver(ChatServiceDbContext dbContext, IMapper mapper)
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
