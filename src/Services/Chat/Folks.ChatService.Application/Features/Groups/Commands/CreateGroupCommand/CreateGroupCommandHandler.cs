using MediatR;

using AutoMapper;

using MongoDB.Bson;

using Folks.ChatService.Application.Features.Channels.Dto;
using Folks.ChatService.Infrastructure.Persistence;
using Folks.ChatService.Domain.Entities;

namespace Folks.ChatService.Application.Features.Groups.Commands.CreateGroupCommand;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, ChannelDto>
{
    private readonly ChatServiceDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateGroupCommandHandler(ChatServiceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<ChannelDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = _mapper.Map<Group>(request);

        _dbContext.Groups.Add(group);
        UpdateUsersGroupIds(group.UserIds, group.Id);

        _dbContext.SaveChanges();

        return Task.FromResult(_mapper.Map<ChannelDto>(group));
    }

    private void UpdateUsersGroupIds(IEnumerable<ObjectId> userIds, ObjectId groupId)
    {
        var users = _dbContext.Users;

        foreach (var user in users)
        {
            if (userIds.Contains(user.Id))
            {
                user.GroupIds.Add(groupId);
            }
        }

        _dbContext.Users.UpdateRange(users);
    }
}
