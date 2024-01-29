// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

using MongoDB.Bson;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class CreateGroupCommandOwnerIdValueResolver : IValueResolver<CreateGroupCommand, Group, ObjectId>
{
    private readonly ChannelsServiceDbContext dbContext;

    public CreateGroupCommandOwnerIdValueResolver(ChannelsServiceDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ObjectId Resolve(CreateGroupCommand source, Group destination, ObjectId destMember, ResolutionContext context)
    {
        var user = this.dbContext.Users.GetBySourceId(source.OwnerId);
        return user.Id;
    }
}
