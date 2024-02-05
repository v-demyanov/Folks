// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Groups.Common.Dto;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class GroupDtoOwnerIdValueResolver : IValueResolver<Group, GroupDto, string>
{
    private readonly ChannelsServiceDbContext dbContext;

    public GroupDtoOwnerIdValueResolver(ChannelsServiceDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public string Resolve(Group source, GroupDto destination, string destMember, ResolutionContext context) =>
        this.dbContext.Users.GetById(source.OwnerId).SourceId;
}
