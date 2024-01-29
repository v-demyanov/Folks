// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

using MongoDB.Bson;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class CreateMessageCommandOwnerIdValueResolver : IValueResolver<CreateMessageCommand, Message, ObjectId>
{
    private readonly ChannelsServiceDbContext dbContext;

    public CreateMessageCommandOwnerIdValueResolver(ChannelsServiceDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ObjectId Resolve(CreateMessageCommand source, Message destination, ObjectId destMember, ResolutionContext context)
    {
        var owner = this.dbContext.Users.GetBySourceId(source.OwnerId);
        return owner.Id;
    }
}
