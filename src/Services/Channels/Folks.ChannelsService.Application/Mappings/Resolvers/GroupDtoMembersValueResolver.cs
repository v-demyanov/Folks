// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Groups.Common.Dto;
using Folks.ChannelsService.Application.Features.Users.Common.Dto;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class GroupDtoMembersValueResolver : IValueResolver<Group, GroupDto, IEnumerable<UserDto>>
{
    private readonly ChannelsServiceDbContext dbContext;
    private readonly IMapper mapper;

    public GroupDtoMembersValueResolver(ChannelsServiceDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public IEnumerable<UserDto> Resolve(Group source, GroupDto destination, IEnumerable<UserDto> destMember, ResolutionContext context)
    {
        var users = this.dbContext.Users.GetByGroupId(source.Id);
        return this.mapper.Map<IEnumerable<UserDto>>(users);
    }
}
