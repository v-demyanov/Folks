// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

using MediatR;
using MongoDB.Bson;

namespace Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, ChannelDto>
{
    private readonly ChannelsServiceDbContext dbContext;
    private readonly IMapper mapper;

    public CreateGroupCommandHandler(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<ChannelDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = this.mapper.Map<Group>(request);

        this.dbContext.Groups.Add(group);
        this.UpdateUsersGroupIds(group.UserIds, group.Id);

        this.dbContext.SaveChanges();

        return Task.FromResult(this.mapper.Map<ChannelDto>(group));
    }

    private void UpdateUsersGroupIds(IEnumerable<ObjectId> userIds, ObjectId groupId)
    {
        var users = this.dbContext.Users.GetByIds(userIds);

        foreach (var user in users)
        {
            user.GroupIds.Add(groupId);
        }

        this.dbContext.Users.UpdateRange(users);
    }
}
