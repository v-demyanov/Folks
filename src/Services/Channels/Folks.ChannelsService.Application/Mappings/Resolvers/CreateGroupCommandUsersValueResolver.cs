// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

using MongoDB.Bson;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class CreateGroupCommandUsersValueResolver : IValueResolver<CreateGroupCommand, Group, ICollection<ObjectId>>
{
    private readonly ChannelsServiceDbContext dbContext;

    public CreateGroupCommandUsersValueResolver(ChannelsServiceDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ICollection<ObjectId> Resolve(CreateGroupCommand source, Group destination, ICollection<ObjectId> destMember, ResolutionContext context)
    {
        var users = this.dbContext.Users.GetBySourceIds(source.UserIds);

        return users.AsEnumerable().Select(user => user.Id).ToList();
    }
}